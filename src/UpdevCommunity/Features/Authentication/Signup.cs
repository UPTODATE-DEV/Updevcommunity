using FastEndpoints;
using FastEndpoints.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Authentication.Signup
{
    public class Request
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;
        public string ProfilePic { get; set; } = string.Empty;

        public string Role { get; set; } = UserRole.Guest;
    }
    public class Response
    {
        public string Message { get; set; } = $"Veuillez confirmer votre compte. " +
            $"Vérifier votre boit email";
    }
    public class SingupValidator : Validator<Request>
    {
        public SingupValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => x.Phone is null)
                .WithMessage("Veuillez spécifier un mail ou numéro de téléphone");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Veuillez spécifier un mot de passe");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Veuillez spécifier un prénom");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Veuillez spécifier un postnom");
        }
    }
    public class Signup : Endpoint<Request, Response>
    {
        private readonly UserManager<UpdevUser> _userManager;
        private readonly IEmailSender _emailSender;
        public Signup(IEmailSender emailSender, UserManager<UpdevUser> userManager)
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/auth/signup");
            Roles(UserRole.Admin);
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var user = new UpdevUser()
            {
                Email = req.Email,
                PhoneNumber = req.Phone,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Role = req.Role,
                ProfilePic = req.ProfilePic,
                State = UserState.Active,
                UserName = string.IsNullOrEmpty(req.Email) ? req.Phone : req.Email
        };
            var result = await CreateUser(user, req.Password);
            if (!result.error.IsEmpty())
            {
                ThrowError(result.error.Message);
                return;
            }
            await SendEmailAndPhoneConfirmation(user);
            await SendAsync(new Response());
        }

        async Task<(Error error, UpdevUser? user)> CreateUser(UpdevUser user, string password)
        {
            if ((user.Role == UserRole.Admin || user.Role == UserRole.SuperAdmin) && !User.IsAdmin())
                return (new Error("Vous n'êtes pas autorisé à créer des utilisateurs administrateurs"),null);

            if (user.Role == UserRole.Moderator && !User.IsAdmin())
                return (new Error("Vous n'êtes pas autorisé à créer un modérateur"),null);

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(user, user.Role);
                return (Error.Empty(),user);
            }
            var errors = result.ToStringErrors();
            return (new Error(errors),null);
        }

        Task<bool> SendEmailAndPhoneConfirmation(UpdevUser user)
            => _userManager.SendConfirmationEmail(_emailSender, user, BaseURL);
    }
}

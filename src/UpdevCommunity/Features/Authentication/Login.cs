using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Validation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Authentication.Login
{
    public class Request
    {
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;
    }
    public class Response
    {
        public string Token { get; set; } = null!;
        public UpdevUser User { get; set; } = null!;
    }
    public class LoginValidator: Validator<Request>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => string.IsNullOrEmpty(x.Phone))
               .WithMessage("Veuillez spécifier un mail ou numéro de téléphone");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Veuillez spécifier un mot de passe");
        }
    }
    public class Login : Endpoint<Request,Response>
    {
        private readonly UserManager<UpdevUser> _userManager;
        private readonly SignInManager<UpdevUser> _signInManager;

        public Login(UserManager<UpdevUser> userManager, SignInManager<UpdevUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/auth/login");
            AllowAnonymous();
        }

        public async override Task HandleAsync(Request req, CancellationToken ct)
        {
            var user = default(UpdevUser);
            if (!string.IsNullOrEmpty(req.Email))
                user = await _userManager.FindByEmailAsync(req.Email);

            if(user is null  && string.IsNullOrEmpty(req.Phone))
                user = _userManager.Users.SingleOrDefault(x => x.PhoneNumber == req.Phone);

            if (user is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password,true);
                if (result.Succeeded)
                {
                    //if (!user.EmailConfirmed && !user.PhoneNumberConfirmed)
                    //    ThrowError("Vous devez confirmer votre compte");

                    if (!(user.State == UserState.Active))
                        ThrowError("Votre compte est desactivé");

                    await SendAsync(new Response()
                    {
                        Token = GenerateToken(user),
                        User = user
                    });
                    return;
                }
            }

            ThrowError("Identifiants incorrect");
            return;
        }

        public string GenerateToken(UpdevUser user)
        {
            var jwtToken = JWTBearer.CreateToken(
               signingKey: "UpdevCommunityjeiwooeiwoei",
               expireAt: DateTime.UtcNow.AddDays(14),
               claims: new[] {(ClaimTypes.Name,user.UserName),(ClaimTypes.NameIdentifier,user.Id),
                               (ClaimTypes.Role, user.Role),(ClaimTypes.Email,user.Email ?? string.Empty),
                              (ClaimTypes.MobilePhone,user.PhoneNumber ?? string.Empty),(ClaimTypes.GivenName,$"{user.FirstName} {user.LastName}")},
               roles: new[] { user.Role});

            return jwtToken;
        }
    }

}

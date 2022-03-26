using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Authentication
{
    public static class AuthExtensions
    {
        public static async Task<bool> SendConfirmationEmail(this UserManager<UpdevUser> userManager, IEmailSender sender, UpdevUser user,string baseUrl)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = $"{baseUrl}Identity/Account/ConfirmEmail?userId={userId}&code={code}";

                await sender.SendEmailAsync(user.Email, "Confirmez votre e-mail",
                    $"Veuillez confirmer votre compte en <a  href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>cliquant ici</a>.");
                return true;
            }

            return false;
        }

        public static async Task<UpdevUser> SendDefaultPassword(this UpdevUser user,string password, IEmailSender emailSender)
        {
            if (!string.IsNullOrEmpty(user.Email))
                await emailSender.SendEmailAsync(user.Email, "UpdevCommunity mot de passe", $"Votre mot de passe par défaut est {password}");
            return user;
        }
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal) 
            => claimsPrincipal.IsInRole(UserRole.Admin) || claimsPrincipal.IsInRole(UserRole.SuperAdmin);

        public static string ToStringErrors(this IdentityResult result)
            => result.Errors
                 .Select(x => x.Description)
                 .Aggregate((current, newVal) => $"{current}{Environment.NewLine}{newVal}");

    }
}

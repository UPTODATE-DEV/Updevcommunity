using Microsoft.AspNetCore.Identity;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Extensions
{
    public static partial class UserExtensions
    {
        public static string? PassGenerate(this UpdevUser user, string password)
        {
            if (password is { Length: < 5 })
                return null;
            var passHash = new PasswordHasher<UpdevUser>();
            return passHash.HashPassword(user, password);
        }
    }
}
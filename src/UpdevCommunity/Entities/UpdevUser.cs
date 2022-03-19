using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpdevCommunity.Entities
{
    public class UserRole
    {
        public const string Guest  = "Visiteur";

        public const string Member  = "Membre";

        public const string Moderator = "Moderateur";

        public const string Admin  = "Admin";

        public const string SuperAdmin = "SuperAdmin";
    }
    public class UserState
    {
        public const string Active = "Active";
        public const string DeActive = "Desactive";
        public const string Suspended = "Suspendu";
    }
    public class UpdevUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => $"{FullName} {LastName}";

        public string ProfilePic { get; set; } = string.Empty;

        public string Role { get; set; } = UserRole.Guest;

        public string State { get; set; } = UserState.Active;

        public DateTime DateOfBirth { get; set; }
    }
}

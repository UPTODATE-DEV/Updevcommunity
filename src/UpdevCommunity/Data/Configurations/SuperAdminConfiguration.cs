using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpdevCommunity.Entities;
using UpdevCommunity.Extensions;

namespace UpdevCommunity.Data.Configurations
{
    public class SuperAdminConfiguration : IEntityTypeConfiguration<UpdevUser>
    {
        private const string superAdminId = "198E6AEA-6827-4562-B415-242146DE9B9B";
        public void Configure(EntityTypeBuilder<UpdevUser> builder)
        {
            var superAdmin = new UpdevUser()
            {
                Id = superAdminId,
                UserName = "superadmin@updevcommunity.com",
                NormalizedUserName = "superadmin@updevcommunity.com".ToUpper(),
                FirstName = "Master",
                LastName = "Admin",
                Email = "superadmin@updevcommunity.com",
                NormalizedEmail = "superAdmin@updevcommunity.com".ToUpper(),
                PhoneNumber = "+243847424020",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = new DateTime(1980,1,1),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = UserRole.SuperAdmin,
                State = UserState.Active
            };

            superAdmin.PasswordHash = superAdmin.PassGenerate("Admin@243Updev");

            builder.HasData(superAdmin);
        }
    }
}
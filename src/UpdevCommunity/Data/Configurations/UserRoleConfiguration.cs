using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UpdevCommunity.Data.Configurations
{
    public class UserRoleConfiguration:IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private const string superAdminRoleId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        private const string superAdminId = "198E6AEA-6827-4562-B415-242146DE9B9B";
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> superAdminToRole = new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            };

            builder.HasData(superAdminToRole);
        }
    }
}
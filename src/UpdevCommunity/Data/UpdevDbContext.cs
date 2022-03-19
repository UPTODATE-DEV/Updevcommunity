using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Data
{
    public class UpdevDbContext : IdentityDbContext<UpdevUser>
    {
        public UpdevDbContext(DbContextOptions<UpdevDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        }
    }
}
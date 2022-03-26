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

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Reply> Replies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reply>().OwnsMany(x => x.Votes);
            builder.Entity<Reply>().OwnsMany(x => x.Images);

            builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        }
    }
}
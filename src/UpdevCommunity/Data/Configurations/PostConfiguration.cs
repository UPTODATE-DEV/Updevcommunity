using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasIndex(x => x.Title).IsUnique();
            builder.OwnsMany(x => x.Votes);
            builder.OwnsMany(x => x.Images);
        }
    }
}

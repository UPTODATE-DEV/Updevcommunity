using System.Collections.ObjectModel;

namespace UpdevCommunity.Entities
{
    public record Tag(string Value);
    public class Image
    {
        public string Link { get; set; } = null!;
        public string Title { get; set; } = null!;  
    }
    public class Post:BaseEntity
    {
        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string? Tags { get; set; }

        public string UserId { get; set; } = null!;
        public UpdevUser? User { get; set; }

        public ICollection<Vote> Votes { get; set; } = new Collection<Vote>();

        public ICollection<Image> Images { get; set; } = new Collection<Image>();

        public ICollection<Reply> Replies { get; set; } = new Collection<Reply>();
    }
}

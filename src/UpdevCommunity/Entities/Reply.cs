using System.Collections.ObjectModel;

namespace UpdevCommunity.Entities
{
    public class Reply:BaseEntity
    {
        public string Body { get; set; } = null!;

        public string? Tags { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }

        public string UserId { get; set; } = null!;
        public UpdevUser? User { get; set; }

        public ICollection<Vote> Votes { get; set; } = new Collection<Vote>();

        public ICollection<Image> Images { get; set; } = new Collection<Image>();

        public int? ReplyParentId { get; set; }
        public Reply? ReplyParent { get; set; }
        public ICollection<Reply> Replies { get; set; } = new Collection<Reply>();
    }
}

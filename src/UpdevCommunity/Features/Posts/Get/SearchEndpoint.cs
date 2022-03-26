using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Posts.Get.Search
{
    public class Search
    {
        public const string InBody = "Body";
        public const string InTitle = "Title";
        public const string InReplies = "Replies";
    }
    public class Request
    {
        public string Query { get; set; } = null!;

        public string? SearchIn { get; set; }
        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;

        public string? Tags { get; set; }

        public string? UserId { get; set; }

        public IEnumerable<Tag> TagList => Tags?.Split(',').Select(x => new Tag(x)) ?? new List<Tag>();
    }
    public class Response
    {
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
    public class SearchEndpoint : Endpoint<Request, Response>
    {
        private readonly UpdevDbContext _dbContext;

        public SearchEndpoint(UpdevDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/posts/search");
            AllowAnonymous();
        }
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var query = _dbContext.Posts.Include(x => x.Images)
            .Include(x => x.Votes).Include(x => x.Replies);
            var queryWithSearch = req.SearchIn switch
            {
                Search.InTitle => query.Where(x => x.Title.Contains(req.Query)),
                Search.InBody => query.Where(x => x.Body.Contains(req.Query)),
                _ => query.Where(x => x.Title.Contains(req.Query) || x.Body.Contains(req.Query))
            };
            var posts = await queryWithSearch
            .Where(x => string.IsNullOrEmpty(req.UserId) || x.UserId == req.UserId)
            .Where(x => string.IsNullOrEmpty(req.Tags) || (string.IsNullOrEmpty(x.Tags)
                || req.TagList.Any(tag => x.Tags.Contains(tag.Value))))
            .Skip(req.Skip).Take(req.Take)
            .ToListAsync();
            await SendAsync(new Response() { Posts = posts });
        }
    }
}

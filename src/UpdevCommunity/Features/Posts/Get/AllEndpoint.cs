using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Posts.Get.AllEndpoint
{
    public class Request
    {
        public int? Skip { get; set; } = 0;

        public int? Take { get; set; } = 10;

        public string? Tags { get; set; }

        public string? UserId { get; set; }

        public IEnumerable<Tag> TagList => Tags?.Split(',').Select(x => new Tag(x)) ?? new List<Tag>();
    }
    public class Response
    {
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
    public class GetAllEndpoint : Endpoint<Request, Response>
    {
        private readonly UpdevDbContext _dbContext;

        public GetAllEndpoint(UpdevDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/posts");
            AllowAnonymous();
        }
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var posts = await _dbContext.Posts.Include(x => x.Images)
            .Include(x => x.Votes).Include(x => x.Replies)
            .Where(x => string.IsNullOrEmpty(req.UserId) || x.UserId == req.UserId)
            .Where(x => string.IsNullOrEmpty(req.Tags) || (string.IsNullOrEmpty(x.Tags)
                || req.TagList.Any(tag => x.Tags.Contains(tag.Value))))
            .Skip(req.Skip ?? 0).Take(req.Take ?? 0)
            .ToListAsync();
            await SendAsync(new Response() { Posts = posts });
        }
    }
}

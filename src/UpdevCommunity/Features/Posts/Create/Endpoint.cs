using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Posts.Create
{
    public class Request
    {
        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string? Tags { get; set; }

        public IEnumerable<Image> Images { get; set; } = new List<Image>();

        public string UserId { get; set; } = null!;
    }
    public class Response
    {
        public Post Post { get; set; } = null!;
    }
    public class CreateEndpoint : Endpoint<Request,Response>
    {
        private readonly UpdevDbContext _dbContext;
        public CreateEndpoint(UpdevDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/posts");
            AllowAnonymous();
            AuthSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var post = new Post()
            {
                Title = req.Title,
                Body = req.Body,
                Tags = req.Tags,
                UserId = req.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Images = req.Images.ToList()
            };
            var savedPost = await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { Post = savedPost.Entity});
        }
    }
}

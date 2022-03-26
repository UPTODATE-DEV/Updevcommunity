using FastEndpoints;
using FastEndpoints.Validation;
using Microsoft.EntityFrameworkCore;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

namespace UpdevCommunity.Features.Posts.Get.ByIdAndTitle
{
    public class Request
    {
        public int? Id { get; set; } 

        public string? Title { get; set; }

    }
    public class Response
    {
        public Post Post { get; set; } = null!;
    }

    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().When(x => string.IsNullOrEmpty(x.Title))
               .WithMessage("Veuillez spécifier l'Id ou Titre de l'article");
        }
    }
    public class ByIdOrTitleEndpoint : Endpoint<Request, Response>
    {
        private readonly UpdevDbContext _dbContext;

        public ByIdOrTitleEndpoint(UpdevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/posts/id");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var post = await _dbContext.Posts.Include(x => x.Images)
           .Include(x => x.Votes).Include(x => x.Replies)
           .Where(x => req.Id == null || x.Id == req.Id)
           .Where(x => string.IsNullOrEmpty(req.Title) || x.Title == req.Title)
           .FirstAsync();
            await SendAsync(new Response() { Post = post});
        }
    }
}

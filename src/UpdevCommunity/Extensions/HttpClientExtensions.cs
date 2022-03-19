namespace UpdevCommunity.Server.Extensions
{
    public static class HttpClientExtensions
    {
        public static string BaseUrl(this HttpContext httpContext)
            => $"{httpContext!.Request.Scheme}://{httpContext!.Request.Host.Value}";
    }
}

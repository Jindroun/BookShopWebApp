using Microsoft.AspNetCore.Builder;
using WebApi.Middleware;
using WebMVC.Middleware;

namespace WebApi.Extensions;

public static class WebAppExtensions
{
    public static void AddMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<TokenAuthenticationMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi.Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<TokenAuthenticationMiddleware> logger;
    private readonly IConfiguration configuration;

    public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger, IConfiguration configuration)
    {
        this.next = next;
        this.logger = logger;
        this.configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            logger.LogWarning("Attempt to access protected resource without token");
            await context.Response.WriteAsync("Authorization header is missing");
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = 401;
            logger.LogWarning("Attempt to access protected resource with empty token");
            await context.Response.WriteAsync("Token is missing");
            return;
        }

        var hardcodedToken = configuration["Token"];

        if (!token.Equals(hardcodedToken))
        {
            context.Response.StatusCode = 401;
            logger.LogWarning("Attempt to access protected resource with invalid token");
            await context.Response.WriteAsync("Invalid token");
            return;
        }

        logger.LogInformation("Token is valid");

        await next(context);
    }
}

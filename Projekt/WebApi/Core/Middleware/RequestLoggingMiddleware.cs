using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebMVC.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestLoggingMiddleware> logger;
    private readonly LiteDatabase db;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, LiteDatabase database)
    {
        this.next = next;
        this.logger = logger;
        this.db = database;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

        bool isApi = endpoint?.Metadata?.GetMetadata<ApiControllerAttribute>() != null;

        string source = isApi ? "API" : "MVC";

        logger.LogInformation("[{Source}] Received request: {Method} {Path}", source, context.Request.Method, context.Request.Path);

        var requestLog = new RequestLog
        {
            Timestamp = DateTime.UtcNow,
            Source = source,
            Method = context.Request.Method,
            Path = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            UserIp = context.Connection.RemoteIpAddress?.ToString()
        };

        var logsCollection = db.GetCollection<RequestLog>("request_logs");
        logsCollection.Insert(requestLog);

        await next(context);
    }
}

public class RequestLog
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public string Method { get; set; }
    public string Path { get; set; }
    public int? StatusCode { get; set; }
    public string? UserIp { get; set; }
}

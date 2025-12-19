namespace dsr_web_api.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeader = "X-Api-Key"; // header name you expect

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // Get API Key from appsettings.json
        string? expectedApiKey = config["ApiKey"];

        // If no expected key is set, allow all (or throw error)
        if (string.IsNullOrWhiteSpace(expectedApiKey))
        {
            await _next(context);
            return;
        }

        // Check if header exists
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var receivedKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        // Compare the received key
        if (receivedKey != expectedApiKey)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        // Continue if key matches
        await _next(context);
    }
}

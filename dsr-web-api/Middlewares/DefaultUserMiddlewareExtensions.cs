namespace dsr_web_api.Middlewares;

public static class DefaultUserMiddlewareExtensions
{
    public static IApplicationBuilder UseDefaultUser(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DefaultUserMiddleware>();
    }
}

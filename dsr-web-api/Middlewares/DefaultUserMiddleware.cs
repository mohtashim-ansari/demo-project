using dsr_web_api.Data;
using dsr_web_api.Models;

namespace dsr_web_api.Middlewares;

public class DefaultUserMiddleware
{
    private readonly RequestDelegate _next;

    public DefaultUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = context.RequestServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!db.UsersInfos.Any())
        {
            var defaultUser = new UsersInfo
            {
                FirstName = "Mohtashim",
                LastName = "Ansari",
                UserName = "admin",
                Email = "admin@example.com",
                PasswordHash = "Admin@123",
                Mobile = "9075903590",
                UserRoleId = 1,
                IsActive = true,
                CreatedBy = 0,
                CreatedOn = DateTime.Now
            };

            db.UsersInfos.Add(defaultUser);
            await db.SaveChangesAsync();
        }

        await _next(context);
    }
}

using dsr_web_api.Data;
using dsr_web_api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.EndPoints;

public static class AccountEndpoints
{
    public static RouteGroupBuilder MapAttandanceInfoEndpoints(this WebApplication app)
    {
        // Main route group: /account
        var group = app.MapGroup("account");

        // GET login /account
        group.MapGet("/login", async (string userName, string password, AppDbContext dbContext) =>
        {
            var user = await dbContext.UsersInfos
                          .Where(x => x.UserName == userName && !x.IsDeleted)
                          .FirstOrDefaultAsync();

            if (user == null)
                return Results.NotFound(new { message = "User not found" });

            var helper = new PasswordHelper();

            bool isMatch = helper.VerifyPassword(user.PasswordHash, password);

            if (!isMatch)
                return Results.BadRequest(new { message = "Invalid password" });

            // SUCCESS -> return user info or token
            return Results.Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.Id,
                    user.FirtName,
                    user.LastName,
                    user.UserName
                }
            });
        });
        return group;
    }
}

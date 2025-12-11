using dsr_web_api.Data;
using dsr_web_api.Models;
using dsr_web_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.EndPoints;

public static class AccountEndpoints
{
    public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
    {
        // Main route group: /account
        var group = app.MapGroup("account");

        // -------------------------------
        // LOGIN APIS
        // -------------------------------
        // POST login /account/login
        group.MapPost("/login", async (LoginRequest request, AppDbContext dbContext, PasswordService passwordService) =>
        {
            var user = await dbContext.UsersInfos
                        .Where(x => x.UserName == request.UserName && !x.IsDeleted)
                        .FirstOrDefaultAsync();

            if (user == null)
                return Results.NotFound(new { message = "User not found." });

            //var helper = new PasswordHelper();
            bool isMatch = passwordService.Verify(user,user.PasswordHash, request.Password);

            if (!isMatch)
                return Results.BadRequest(new { message = "Invalid password." });

            return Results.Ok(new
            {
                message = "Login successful.",
                user
            });
        });
        return group;
    }
}

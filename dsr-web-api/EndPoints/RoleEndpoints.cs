using System;
using dsr_web_api.Data;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.EndPoints;

public static class RoleEndpoints
{
    public static RouteGroupBuilder MapRoleEndpoints(this WebApplication app)
    {
        // Main route group: /role
        var group = app.MapGroup("role");
        
        // GET /role
        group.MapGet("/", async (AppDbContext db) =>
            await db.UserRoles
                .ToListAsync()
        );

        // GET /role/permission/{id}
        group.MapGet("/permission/{id}", async (int id, AppDbContext dbContext) =>
        {
            var rolePermissions = await dbContext.RolePermissions.Where(x => x.RoleId == id).ToListAsync();

            if (rolePermissions is null)
                return Results.NotFound("User not found");

            return Results.Ok(rolePermissions);
        });

        return group;
    }
}

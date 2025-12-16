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

        // -------------------------------
        // DEFAULT CRUD APIS
        // -------------------------------
        // GET /role
        group.MapGet("/", async (AppDbContext db) =>
            await db.UserRoles
                .ToListAsync()
        );

        return group;
    }
}

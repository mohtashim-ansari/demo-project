using dsr_web_api.Data;
using dsr_web_api.Dtos;
using dsr_web_api.Mapping;
using dsr_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.EndPoints;

public static class UsersInfoEndpoints
{
    public static RouteGroupBuilder MapUsersInfoEndpoints(this WebApplication app)
    {
        // Main route group: /usersinfo
        var group = app.MapGroup("usersinfo");

        // GET /usersinfo
        group.MapGet("/", async (AppDbContext db) =>
            await db.UsersInfos
                .Where(x => !x.IsDeleted)    // Return only active users
                .ToListAsync()
        );

        // GET /usersinfo/{id}
        group.MapGet("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var users = await dbContext.UsersInfos.FindAsync(id);

            if (users is null)
                return Results.NotFound("User not found");

            return Results.Ok(users);
        });

        // POST /usersinfo
        group.MapPost("/", async (CreateUsersInfoDto dto, AppDbContext db) =>
        {
            // Create new user entity from DTO
            UsersInfo user = dto.ToEntity();

            db.UsersInfos.Add(user);
            await db.SaveChangesAsync();

            return Results.Created($"/usersinfo/{user.Id}", user);
        });

        // PUT /usersinfo/{id}
        group.MapPut("/{id}", async (int id, UpdateUsersInfoDto dto, AppDbContext dbContext) =>
       {
           var existingUser = await dbContext.UsersInfos.FindAsync(id);

           if (existingUser is null)
               return Results.NotFound("User not found");

           dbContext.Entry(existingUser).CurrentValues.SetValues(dto.ToEntity(id));

           await dbContext.SaveChangesAsync();
           return Results.NoContent();
       });

        // DELETE (SOFT) /usersinfo/{id}
        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var user = await dbContext.UsersInfos.FindAsync(id);

            if (user is null)
                return Results.NotFound("User not found");

            // Soft delete by marking IsDeleted
            user.IsDeleted = true;
            //user.UpdatedBy = 0;
            user.UpdatedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        return group;
    }
}

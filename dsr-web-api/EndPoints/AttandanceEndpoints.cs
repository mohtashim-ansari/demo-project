using dsr_web_api.Data;
using dsr_web_api.Dtos;
using dsr_web_api.Mapping;
using dsr_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.EndPoints;

public static class AttandanceInfoEndpoints
{
    public static RouteGroupBuilder MapAttandanceInfoEndpoints(this WebApplication app)
    {
        // Main route group: /attandanceinfo
        var group = app.MapGroup("attandanceinfo");

        // GET ALL /attandanceinfo
        group.MapGet("/", async (AppDbContext db) =>
            await db.AttandanceInfos
                .Where(x => !x.IsDeleted)
                .ToListAsync()
        );

        // GET SINGLE /attandanceinfo/{id}
        group.MapGet("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var attandance = await dbContext.AttandanceInfos.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();

            if (attandance is null)
                return Results.NotFound("Attendance record not found");

            return Results.Ok(attandance);
        });

        // POST /attandanceinfo
        group.MapPost("/", async (CreateAttandanceDto dto, AppDbContext db) =>
        {
            AttandanceInfo attandance = dto.ToEntity();

            db.AttandanceInfos.Add(attandance);
            await db.SaveChangesAsync();

            return Results.Created($"/attandanceinfo/{attandance.Id}", attandance);
        });

        // PUT /attandanceinfo/{id}
        group.MapPut("/{id}", async (int id, UpdateAttandaceDto dto, AppDbContext dbContext) =>
        {
            var existing = await dbContext.AttandanceInfos.FindAsync(id);

            if (existing is null)
                return Results.NotFound("Attendance record not found");

            dbContext.Entry(existing).CurrentValues.SetValues(dto.ToEntity(id, existing));

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // SOFT DELETE /attandanceinfo/{id}
        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var attandance = await dbContext.AttandanceInfos.FindAsync(id);

            if (attandance is null)
                return Results.NotFound("Attendance record not found");

            attandance.IsDeleted = true;
            //attandance.UpdatedBy = 0;
            attandance.UpdatedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapGet("todays/{id}", async (int id, AppDbContext dbContext) =>
        {
            var todaysAttandance = await dbContext.AttandanceInfos.Where(x => x.UserId == id && x.AttandanceDate.Date == DateTime.Now.Date && !x.IsDeleted).FirstOrDefaultAsync();

            if (todaysAttandance is null)
                return Results.NotFound("Attendance record not found");

            return Results.Ok(todaysAttandance);
        });

        return group;
    }
}

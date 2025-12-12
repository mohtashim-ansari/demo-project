using dsr_web_api.Data;
using dsr_web_api.Dtos;
using dsr_web_api.Mapping;
using dsr_web_api.Models;
using Microsoft.EntityFrameworkCore;

public static class AttandanceInfoEndpoints
{
    public static RouteGroupBuilder MapAttandanceInfoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("attandanceinfo");

        // -------------------------------
        // DEFAULT CRUD APIS
        // -------------------------------
        group.MapGet("/", async (AppDbContext db) =>
            await db.AttandanceInfos
                .Where(x => !x.IsDeleted)
                .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var attandance = await dbContext.AttandanceInfos
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (attandance is null)
                return Results.NotFound("Attendance record not found");

            return Results.Ok(attandance);
        });

        group.MapPost("/", async (CreateAttandanceDto dto, AppDbContext db) =>
        {
            AttandanceInfo attandance = dto.ToEntity();

            db.AttandanceInfos.Add(attandance);
            await db.SaveChangesAsync();

            return Results.Created($"/attandanceinfo/{attandance.Id}", attandance);
        });

        group.MapPut("/{id}", async (int id, UpdateAttandaceDto dto, AppDbContext dbContext) =>
        {
            var existing = await dbContext.AttandanceInfos.FindAsync(id);

            if (existing is null)
                return Results.NotFound("Attendance record not found");

            dbContext.Entry(existing).CurrentValues.SetValues(dto.ToEntity(id, existing));

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
        {
            var attandance = await dbContext.AttandanceInfos.FindAsync(id);

            if (attandance is null)
                return Results.NotFound("Attendance record not found");

            attandance.IsDeleted = true;
            attandance.UpdatedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // -------------------------------
        // CUSTOM APIS
        // -------------------------------
        // GET /attandanceinfo/todays/{id}
        group.MapGet("todays/{id}", async (int id, AppDbContext dbContext) =>
        {
            var todaysAttandance = await dbContext.AttandanceInfos.Where(x => x.UserId == id && x.AttandanceDate.Date == DateTime.Now.Date && !x.IsDeleted).FirstOrDefaultAsync();

            if (todaysAttandance is null)
                return Results.NotFound("Attendance record not found");

            return Results.Ok(todaysAttandance);
        });

        // GET ALL USERS WITH TODAY'S ATTENDANCE
        group.MapGet("todays/all", async (AppDbContext dbContext) =>
            {
                var result = await dbContext.UsersInfos
                .Where(u => !u.IsDeleted)
                .Join(
                    dbContext.AttandanceInfos,
                    u => u.Id,
                    a => a.UserId,
                    (u, a) => new
                    {
                        u.Id,
                        u.FirtName,
                        u.LastName,
                        u.Email,
                        u.Mobile,
                        a.AttandanceDate,
                        a.IsPresent
                    }
                )
                .Where(x => x.AttandanceDate.Date == DateTime.Now.Date)
                .ToListAsync();

                return Results.Ok(result);
            });

        return group;
    }
}

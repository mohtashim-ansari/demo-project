using dsr_web_api.Data;
using dsr_web_api.Dtos;
using dsr_web_api.Mapping;
using dsr_web_api.Models;
using dsr_web_api.Services;
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

        group.MapPut("/{id}", async (int id, UpdateAttendanceDto dto, AppDbContext dbContext) =>
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
                    a.Id,
                    u.FirstName,
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

        group.MapPut("/update", async (UpdateAttendanceDto dto, AppDbContext dbContext) =>
        {
            var attendance = await dbContext.AttandanceInfos.FindAsync(dto.Id);
            if (attendance is null) return Results.NotFound();

            attendance.IsPresent = attendance.IsPresent ? false : true; // Toggle IsPresent
            attendance.UpdatedBy = 1;
            attendance.UpdatedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return Results.Ok(attendance);
        });


        // -------------------------------
        // GET ALL USERS WITH ALL ATTENDANCE (SEARCH PAGE)
        // -------------------------------
        group.MapGet("all", async (AppDbContext dbContext) =>
        {
            var result = await dbContext.UsersInfos
                .Where(u => !u.IsDeleted)
                .Join(
                    dbContext.AttandanceInfos.Where(a => !a.IsDeleted),
                    u => u.Id,
                    a => a.UserId,
                    (u, a) => new
                    {
                        a.Id,
                        u.FirstName,
                        u.LastName,
                        u.Email,
                        u.Mobile,
                        a.AttandanceDate,
                        a.IsPresent,
                        a.IsDSRSent
                    }
                )
                .OrderByDescending(x => x.AttandanceDate)
                .ToListAsync();

            return Results.Ok(result);
        });

        // -------------------------------
        // GET ALL ATTENDANCE WITH FILTERS (NAME + DATE RANGE)
        // -------------------------------
        group.MapGet("all/search", async (
            AppDbContext db,
            string? name,
            DateTime? fromDate,
            DateTime? toDate) =>
        {
            var query = db.UsersInfos
                .Where(u => !u.IsDeleted)
                .Join(
                    db.AttandanceInfos.Where(a => !a.IsDeleted),
                    u => u.Id,
                    a => a.UserId,
                    (u, a) => new
                    {
                        a.Id,
                        u.FirstName,
                        u.LastName,
                        u.Email,
                        u.Mobile,
                        a.AttandanceDate,
                        a.IsPresent,
                        a.IsDSRSent
                    });

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x =>
                    (x.FirstName + " " + x.LastName).Contains(name));

            if (fromDate.HasValue)
                query = query.Where(x => x.AttandanceDate.Date >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(x => x.AttandanceDate.Date <= toDate.Value.Date);

            return Results.Ok(
                await query
                    .OrderByDescending(x => x.AttandanceDate)
                    .ToListAsync()
            );
        });
        group.MapPut("/dsrreminder", async (UpdateAttendanceDto dto, AppDbContext dbContext, EmailService emailService) =>
        {
            var attendance = await dbContext.AttandanceInfos.FindAsync(dto.Id);
            if (attendance is null) return Results.NotFound();

            var user = await dbContext.UsersInfos.FindAsync(attendance.UserId);
            if (user is null) return Results.NotFound();

            if (attendance.IsPresent && !attendance.IsDSRSent)
            {
                try
                {
                    emailService.Send(
                        user.Email,
                        "DSR Pending Reminder",
                        $"""
                <p>Hi {user.FirstName},</p>
                <p>You are marked <b>Present</b> today, but your <b>DSR has not been submitted</b>.</p>
                <p>Please submit your DSR as soon as possible.</p>
                """
                    );
                    Console.WriteLine($"DSR reminder sent to {user.Email}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email to {user.Email}");
                    Console.WriteLine(ex.Message);
                }
            }          

            return Results.Ok(attendance);
        });

        return group;
    }
}

using System;
using dsr_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dsr_web_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UsersInfo> UsersInfos => Set<UsersInfo>();
    public DbSet<AttandanceInfo> AttandanceInfos => Set<AttandanceInfo>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, Name = "SuperAdmin", Description = "Super Admin", IsDefault = true },
            new UserRole { Id = 2, Name = "Admin", Description = "Admin", IsDefault = false },
            new UserRole { Id = 3, Name = "Employee", Description = "Employee", IsDefault = false }
        );
    }
}

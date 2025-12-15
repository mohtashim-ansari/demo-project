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
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, Name = "SuperAdmin", Description = "Super Admin", IsDefault = true },
            new UserRole { Id = 2, Name = "Admin", Description = "Admin", IsDefault = false },
            new UserRole { Id = 3, Name = "Employee", Description = "Employee", IsDefault = false }
        );

        modelBuilder.Entity<RolePermission>().HasData(
            new RolePermission { Id = 1, RoleId = 1, PageName = "" },
            new RolePermission { Id = 2, RoleId = 1, PageName = "" },
            new RolePermission { Id = 3, RoleId = 1, PageName = "" }
        );
    }
}

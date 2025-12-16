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
            // Super Admin
            new RolePermission { Id = 1, RoleId = 1, PageName = "Home" },
            new RolePermission { Id = 2, RoleId = 1, PageName = "Attendance" },
            new RolePermission { Id = 3, RoleId = 1, PageName = "Search" },
            new RolePermission { Id = 4, RoleId = 1, PageName = "DSR" },
            new RolePermission { Id = 5, RoleId = 1, PageName = "Registration" },
            // Admin
            new RolePermission { Id = 6, RoleId = 2, PageName = "Home" },
            new RolePermission { Id = 7, RoleId = 2, PageName = "Attendance" },
            new RolePermission { Id = 8, RoleId = 2, PageName = "Search" },
            new RolePermission { Id = 9, RoleId = 2, PageName = "DSR" },
            // Employee
            new RolePermission { Id = 10, RoleId = 3, PageName = "Home" },
            new RolePermission { Id = 11, RoleId = 3, PageName = "Attendance" }
        );
    }
}

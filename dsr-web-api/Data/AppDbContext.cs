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

}

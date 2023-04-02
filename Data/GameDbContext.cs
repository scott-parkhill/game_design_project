using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Chaos.Models.DbModels;

namespace Chaos.Data;

public class GameDbContext : IdentityDbContext<GameUser>
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<GameUser> GameUsers => Set<GameUser>();
    public DbSet<Army> Armies => Set<Army>();
    public DbSet<AfterActionReport> AfterActionReports => Set<AfterActionReport>();
    public DbSet<SpyReport> SpyReports => Set<SpyReport>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Set foreign keys and other database parameters.
        builder.Entity<Army>().HasKey(u => new { u.UserId });
        builder.Entity<SpyReport>().HasKey(u => new { u.SapperId, u.SentryId });
        builder.Entity<AfterActionReport>().HasOne<GameUser>().WithMany().HasForeignKey(u => u.AggressorId);
        builder.Entity<AfterActionReport>().HasOne<GameUser>().WithMany().HasForeignKey(u => u.DefenderId);

    }
}

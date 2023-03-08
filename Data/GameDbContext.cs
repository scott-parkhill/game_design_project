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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Set foreign keys and other database parameters.
        builder.Entity<Army>().HasKey(u => new { u.UserId });
    }
}

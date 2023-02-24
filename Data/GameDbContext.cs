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
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chaos.Data;

public class GameDbContext : IdentityDbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }
}

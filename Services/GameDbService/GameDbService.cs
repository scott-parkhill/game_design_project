using Chaos.Data;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    readonly GameDbContext _context;
    readonly ILogger<GameDbService> _logger;
    public GameDbService(GameDbContext context, ILogger<GameDbService> logger) => (_context, _logger) = (context, logger);
}

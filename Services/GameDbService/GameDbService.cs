using Chaos.Data;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    readonly GameDbContext _context;
    readonly Logger<GameDbService> _logger;
    public GameDbService(GameDbContext context, Logger<GameDbService> logger) => (_context, _logger) = (context, logger);
}

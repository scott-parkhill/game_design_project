using Chaos.Business;
using Microsoft.EntityFrameworkCore;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    public async Task<Factions> GetUserFaction(string loggedUserId)
    {
        var user = await _context.GameUsers.Where(u => u.Id == loggedUserId).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new ArgumentException("The given user does not exist in the database.", nameof(loggedUserId));
        }

        return user.Faction;
    }
}

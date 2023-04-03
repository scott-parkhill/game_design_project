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

    public async Task<Dictionary<string, string>> GetUsernamesFromIds(IEnumerable<string> userIds)
    {
        return await _context.GameUsers.Where(u => userIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id, v => v.UserName!);
    }

    public async Task<string> GetUserIdFromUsername(string username)
    {
        var userId = await _context.GameUsers.Where(u => u.UserName == username).Select(u => u.Id).FirstOrDefaultAsync();

        if (userId is null)
            throw new ArgumentException("Username doesn't exist in the database.");

        return userId;
    }

    public async Task<(string, string)> GetBelligerents(string aggressorId, string defenderId)
    {
        var aggressor = await _context.GameUsers.Where(u => u.Id == aggressorId).Select(u => u.UserName).FirstOrDefaultAsync();
        var defender = await _context.GameUsers.Where(u => u.Id == defenderId).Select(u => u.UserName).FirstOrDefaultAsync();

        if (aggressor is null || defender is null)
            throw new ArgumentException("One of the users doesn't exist in the database.");

        return (aggressor, defender);
    }
}

using Chaos.Models.ViewModels;
using Chaos.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Chaos.Extensions;
using Chaos.Data;
using Chaos.Business;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    public async Task<DbResult> AddArmy(string loggedUserId)
    {
        if (await _context.Armies.AnyAsync(u => u.UserId == loggedUserId))
            return new(TaskResults.Invalid, "User already has an army.");

        Army army = new()
        {
            UserId = loggedUserId,
            Recruits = 20
        };

        await _context.AddAsync(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add army to the database.");
        }

        return new(TaskResults.Success, "Army successfully added to database.");
    }

    public async Task<ArmyViewModel?> GetArmyViewModel(string userId)
    {
        return await _context.Armies.Where(u => u.UserId == userId).ToViewModel().FirstOrDefaultAsync();
    }

    // TODO This is hacktastic, fix.
    public async Task<DbResult> UpdateArmies()
    {
        var armies = await _context.Armies.ToListAsync();

        foreach (var army in armies)
        {
            if (army.LastRecruitment.Date < DateTime.UtcNow.Date)
            {
                army.Recruits += army.RecruitRate;
                army.LastRecruitment = DateTime.UtcNow;
            }

            if (army.LastCoinGeneration.Date < DateTime.UtcNow.Date)
            {
                army.Coins += army.CoinGenerationRate;
                army.LastCoinGeneration = DateTime.UtcNow;
            }
        }

        _context.UpdateRange(armies);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add recruits and coins.");
        }

        return new(TaskResults.Success, "Successfully added recruits and coins.");
    }
}

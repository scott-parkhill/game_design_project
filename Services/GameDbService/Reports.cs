using Chaos.Models.DbModels;
using Chaos.Models.ViewModels;
using Chaos.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Chaos.Business;
using Chaos.Extensions;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    #region Spy Reports
    public async Task<DbResult> CreateSpyReport(SpyReportViewModel spyReport)
    {
        if (!(await _context.GameUsers.AnyAsync(u => u.Id == spyReport.SapperId)))
            return new(TaskResults.Invalid, "Sapper doesn't exist in the database.");
        
        if (!(await _context.GameUsers.AnyAsync(u => u.Id == spyReport.SentryId)))
            return new(TaskResults.Invalid, "Sentry doesn't exist in the database.");

        SpyReport newReport = new()
        {
            SapperId = spyReport.SapperId,
            SentryId = spyReport.SentryId,
            SpyTime = spyReport.SpyTime,
            SapperStrength = spyReport.SapperStrength,
            SapperToolsLostJson = JsonSerializer.Serialize(spyReport.SapperToolsLost),
            SentryMinimumDefence = spyReport.SentryMinimumDefence,
            SentryMaximumDefence = spyReport.SentryMaximumDefence,
            SentryToolsLostJson = JsonSerializer.Serialize(spyReport.SentryToolsLost),
            SapperSapperLosses = spyReport.SapperSapperLosses,
            SapperRecruitLosses = spyReport.SapperRecruitLosses,
            SentryRecruitLosses = spyReport.SentryRecruitLosses,
            SentrySentryLosses = spyReport.SentrySentryLosses,
            Outcome = spyReport.Outcome
        };

        _context.Add(newReport);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add new spy report to the database.");
        }

        return new(TaskResults.Success, "Successfully added spy report to the database.");
    }
    
    public async Task<List<SpyReportViewModel>> GetSpyReports(string loggedUserId)
    {
        return await _context.SpyReports.Where(u => u.SapperId == loggedUserId || u.SentryId == loggedUserId).ToViewModel().OrderByDescending(u => u.SpyTime).ToListAsync();
    }

    public async Task<SpyReportViewModel?> GetSpyReport(int id)
    {
        return await _context.SpyReports.Where(u => u.Id == id).ToViewModel().FirstOrDefaultAsync();
    }

    #endregion

    #region After Action Reports

    public async Task<DbResult> CreateAfterActionReport(AfterActionReportViewModel afterActionReport)
    {
        if (!(await _context.GameUsers.AnyAsync(u => u.Id == afterActionReport.AggressorId)))
            return new(TaskResults.Invalid, "Aggressor doesn't exist in the database.");

        if (!(await _context.GameUsers.AnyAsync(u => u.Id == afterActionReport.DefenderId)))
            return new(TaskResults.Invalid, "Defender doesn't exist in the database.");

        AfterActionReport newReport = new()
        {
            AggressorId = afterActionReport.AggressorId,
            DefenderId = afterActionReport.DefenderId,
            BattleTime = afterActionReport.BattleTime,
            AggressorRecruitLosses = afterActionReport.AggressorRecruitLosses,
            AggressorAttackerLosses = afterActionReport.AggressorAttackerLosses,
            AggressorToolsLostJson = JsonSerializer.Serialize(afterActionReport.AggressorToolsLost),
            DefenderCoinLosses = afterActionReport.DefenderCoinLosses,
            DefenderRecruitLosses = afterActionReport.DefenderRecruitLosses,
            DefenderDefenderLosses = afterActionReport.DefenderDefenderLosses,
            DefenderToolsLostJson = JsonSerializer.Serialize(afterActionReport.DefenderToolsLost),
            Outcome = afterActionReport.Outcome
        };

        _context.Add(newReport);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add new after action report to the database.");
        }

        return new(TaskResults.Success, "Successfully added after action report to the database.");
    }

    public async Task<List<AfterActionReportViewModel>> GetAfterActionReports(string loggedUserId)
    {
        return await _context.AfterActionReports.Where(u => u.AggressorId == loggedUserId || u.DefenderId == loggedUserId).ToViewModel().OrderByDescending(u => u.BattleTime).ToListAsync();
    }

    public async Task<AfterActionReportViewModel?> GetAfterActionReport(int id)
    {
        return await _context.AfterActionReports.Where(u => u.Id == id).ToViewModel().FirstOrDefaultAsync();
    }

    #endregion
    
    #region Hiscores

    public async Task<List<(string Username, double ArmyScore, int Coins, Factions Faction)>> GetHiscores()
    {
        var armies = await _context.Armies.ToViewModel().ToListAsync();
        var usernames = await _context.GameUsers.ToDictionaryAsync(u => u.Id, v => v.UserName);
        var userFactions = await _context.GameUsers.ToDictionaryAsync(u => u.Id, v => v.Faction);

        return armies.Select(u => (usernames[u.UserId], ArmyScore: ArmyScore.GetArmyScore(u, userFactions[u.UserId]), u.Coins, userFactions[u.UserId])).OrderByDescending(u => u.ArmyScore).ToList();
    }
    
    #endregion
}

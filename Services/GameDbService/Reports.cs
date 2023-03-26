using Chaos.Models.DbModels;
using Chaos.Models.ViewModels;
using Chaos.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Chaos.Business;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
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
            SentryToolsLostJson = JsonSerializer.Serialize(spyReport.SentryToolsLost)
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
    
    // public async Task<List<SpyReportViewModel>> GetSpyReports(string loggedUserId)
    // {
    //     var reports = await _context.SpyRe
    // }
}
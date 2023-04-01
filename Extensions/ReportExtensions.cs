using Chaos.Models.ViewModels;
using Chaos.Models.DbModels;
using Chaos.Models;
using System.Text.Json;

namespace Chaos.Extensions;

public static class ReportExtensions
{
    public static IQueryable<SpyReportViewModel> ToViewModel(this IQueryable<SpyReport> report)
    {
        return report.Select(u => new SpyReportViewModel()
        {
            Id = u.Id,
            SapperId = u.SapperId,
            SentryId = u.SentryId,
            SpyTime = u.SpyTime,
            SapperStrength = u.SapperStrength,
            SapperToolsLost = JsonSerializer.Deserialize<UserWeaponsData>(u.SapperToolsLostJson ?? "{}", new JsonSerializerOptions()) ?? new(),
            SentryMinimumDefence = u.SentryMinimumDefence,
            SentryMaximumDefence = u.SentryMaximumDefence,
            SentryToolsLost = JsonSerializer.Deserialize<UserWeaponsData>(u.SentryToolsLostJson ?? "{}", new JsonSerializerOptions()) ?? new(),
            SapperRecruitLosses = u.SapperRecruitLosses,
            SapperSapperLosses = u.SapperSapperLosses,
            SentryRecruitLosses = u.SentryRecruitLosses,
            SentrySentryLosses = u.SentrySentryLosses
        });
    }

    public static IQueryable<AfterActionReportViewModel> ToViewModel(this IQueryable<AfterActionReport> report)
    {
        return report.Select(u => new AfterActionReportViewModel()
        {
            Id = u.Id,
            AggressorId = u.AggressorId,
            DefenderId = u.DefenderId,
            BattleTime = u.BattleTime,
            AggressorRecruitLosses = u.AggressorRecruitLosses,
            AggressorAttackerLosses = u.AggressorAttackerLosses,
            AggressorToolsLost = JsonSerializer.Deserialize<UserWeaponsData>(u.AggressorToolsLostJson ?? "{}", new JsonSerializerOptions()) ?? new(),
            DefenderCoinLosses = u.DefenderCoinLosses,
            DefenderRecruitLosses = u.DefenderRecruitLosses,
            DefenderDefenderLosses = u.DefenderDefenderLosses,
            DefenderToolsLost = JsonSerializer.Deserialize<UserWeaponsData>(u.DefenderToolsLostJson ?? "{}", new JsonSerializerOptions()) ?? new()
        });
    }
}

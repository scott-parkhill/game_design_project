using Chaos.Models.DbModels;
using Chaos.Models.ViewModels;

namespace Chaos.Extensions;

public static class ArmyExtensions
{
    public static IQueryable<ArmyViewModel> ToViewModel(this IQueryable<Army> army)
    {
        return army.Select(u => new ArmyViewModel()
        {
            RecruitRate = u.RecruitRate,
            LastRecruitment = u.LastRecruitment,
            Coins = u.Coins,
            CoinGenerationRate = u.CoinGenerationRate,
            LastCoinGeneration = u.LastCoinGeneration,
            Recruits = u.Recruits,
            Attackers = u.Attackers,
            Defenders = u.Defenders
        });
    }    
}

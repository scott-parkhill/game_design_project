using Chaos.Models.DbModels;
using Chaos.Models.ViewModels;
using System.Text.Json;
using Chaos.Models;

namespace Chaos.Extensions;

public static class ArmyExtensions
{
    public static IQueryable<ArmyViewModel> ToViewModel(this IQueryable<Army> army)
    {
        return army.Select(u => new ArmyViewModel()
        {
            UserId = u.UserId,
            RecruitRate = u.RecruitRate,
            LastRecruitment = u.LastRecruitment,
            Coins = u.Coins,
            CoinGenerationRate = u.CoinGenerationRate,
            LastCoinGeneration = u.LastCoinGeneration,
            Recruits = u.Recruits,
            Attackers = u.Attackers,
            Defenders = u.Defenders,
            Sentries = u.Sentries,
            Sappers = u.Sappers,
            UserWeapons = JsonSerializer.Deserialize<UserWeaponsData>(u.UserWeaponsJsonData ?? "{}", new JsonSerializerOptions()) ?? new()
        });
    }    
}

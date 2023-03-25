namespace Chaos.Models.DbModels;

#nullable disable
public class Army
{
    public int Id { get; set; }
    public string UserId { get; set; }

    #region ArmyAdministration
    public int RecruitRate { get; set; } = 10;
    public DateTime LastRecruitment { get; set; } = DateTime.UtcNow;
    public int Coins { get; set; }
    public int CoinGenerationRate { get; set; } = 100;
    public DateTime LastCoinGeneration { get; set; } = DateTime.UtcNow;
    public string UserWeaponsJsonData { get; set; } = "{}";
    #endregion


    #region ArmySoldiers
    public int Recruits { get; set; }
    public int Attackers { get; set; }
    public int Defenders { get; set; }
    public int Sentries { get; set; }
    public int Sappers { get; set; }
    #endregion
}

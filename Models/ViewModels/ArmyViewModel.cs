namespace Chaos.Models.ViewModels;

public class ArmyViewModel
{
    #region ArmyAdministration
    public string UserId { get; set; }
    public int RecruitRate { get; set; } = -1;
    public DateTime LastRecruitment { get; set; } = DateTime.UnixEpoch;
    public int Coins { get; set; } = -1;
    public int CoinGenerationRate { get; set; } = -1;
    public DateTime LastCoinGeneration { get; set; } = DateTime.UnixEpoch;
    public UserWeaponsData UserWeapons { get; set; } = new();
    #endregion


    #region ArmySoldiers
    public int Recruits { get; set; }
    public int Attackers { get; set; }
    public int Defenders { get; set; }
    public int Sentries { get; set; }
    public int Sappers { get; set; }
    #endregion

    public ArmyViewModel Clone() => (ArmyViewModel)this.MemberwiseClone();
}

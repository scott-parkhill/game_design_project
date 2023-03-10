namespace Chaos.Models.ViewModels;

public class ArmyViewModel
{
    #region ArmyAdministration
    public int RecruitRate { get; set; } = -1;
    public DateTime LastRecruitment { get; set; } = DateTime.UnixEpoch;
    public int Coins { get; set; } = -1;
    public int CoinGenerationRate { get; set; } = -1;
    public DateTime LastCoinGeneration { get; set; } = DateTime.UnixEpoch;
    #endregion

    #region ArmySoldiers
    public int Recruits { get; set; }
    public int Attackers { get; set; }
    public int Defenders { get; set; }
    #endregion
}

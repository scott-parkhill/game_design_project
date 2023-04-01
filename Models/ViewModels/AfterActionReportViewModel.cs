namespace Chaos.Models.ViewModels;

public class AfterActionReportViewModel
{
    public int Id { get; set; }
    public string AggressorId { get; set; } = "";
    public string DefenderId { get; set; } = "";
    public DateTime BattleTime { get; set; }


    #region Aggressor
    public int AggressorCoinGains => -1 * DefenderCoinLosses;
    public int AggressorRecruitLosses { get; set; } = -1;
    public int AggressorAttackerLosses { get; set; } = -1;
    public UserWeaponsData AggressorToolsLost { get; set; } = new();
    #endregion


    #region Defender
    public int DefenderCoinLosses { get; set; }
    public int DefenderRecruitLosses { get; set; }
    public int DefenderDefenderLosses { get; set; }
    public UserWeaponsData DefenderToolsLost { get; set; } = new();
    #endregion
}

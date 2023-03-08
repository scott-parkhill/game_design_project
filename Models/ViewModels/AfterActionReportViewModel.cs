namespace Chaos.Models.ViewModels;

public class AfterActionReportViewModel
{
    public string AggressorName { get; set; } = "";
    public string DefenderName { get; set; } = "";

    #region Aggressor
    public int AggressorCoinGains => -1 * DefenderCoinLosses;
    public int AggressorRecruitLosses { get; set; } = -1;
    public int AggressorAttackerLosses { get; set; } = -1;
    #endregion

    #region Defender
    public int DefenderCoinLosses { get; set; }
    public int DefenderRecruitLosses { get; set; }
    public int DefenderDefenderLosses { get; set; }
    #endregion
}

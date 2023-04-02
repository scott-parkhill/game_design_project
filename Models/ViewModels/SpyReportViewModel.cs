namespace Chaos.Models.ViewModels;

public class SpyReportViewModel
{
    public int Id { get; set; }
    public string SapperId { get; set; } = "";
    public string SentryId { get; set; } = "";
    public DateTime SpyTime { get; set; }
    public string Outcome { get; set; } = "";


    #region Sapper
    public double SapperStrength { get; set; }
    public int SapperSapperLosses { get; set; }
    public int SapperRecruitLosses { get; set; }
    public UserWeaponsData SapperToolsLost { get; set; } = new();
    #endregion


    #region Sentry
    public double SentryMinimumDefence { get; set; }
    public double SentryMaximumDefence { get; set; }
    public int SentrySentryLosses { get; set; }
    public int SentryRecruitLosses { get; set; }
    public UserWeaponsData SentryToolsLost { get; set; } = new();
    #endregion
}

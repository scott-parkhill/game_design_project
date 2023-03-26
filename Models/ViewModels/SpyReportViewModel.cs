namespace Chaos.Models.ViewModels;

public class SpyReportViewModel
{
    public string SapperId { get; set; } = "";
    public string SentryId { get; set; } = "";
    public DateTime SpyTime { get; set; }


    #region Sapper
    public double SapperStrength { get; set; }
    public UserWeaponsData SapperToolsLost { get; set; } = new();
    #endregion


    #region Sentry
    public double SentryMinimumDefence { get; set; }
    public double SentryMaximumDefence { get; set; }
    public UserWeaponsData SentryToolsLost { get; set; } = new();
    #endregion
}

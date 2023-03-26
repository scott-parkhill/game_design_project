namespace Chaos.Models.DbModels;

#nullable disable
public class SpyReport
{
    public int Id { get; set; }
    public string SapperId { get; set; }
    public string SentryId { get; set; }
    public DateTime SpyTime { get; set; }


    #region Sapper
    public double SapperStrength { get; set; }
    public string SapperToolsLostJson { get; set; } = "{}";
    #endregion


    #region Sentry
    public double SentryMinimumDefence { get; set; }
    public double SentryMaximumDefence { get; set; }
    public string SentryToolsLostJson { get; set; } = "{}";
    #endregion
}

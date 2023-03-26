namespace Chaos.Models.DbModels;

#nullable disable
public class SpyReport
{
    public int Id { get; set; }
    public string AggressorId { get; set; }
    public string DefenderId { get; set; }


    #region Aggressor
    public double AggressorSapperStrength { get; set; }
    public string SapperToolsLostJson { get; set; } = "{}";
    #endregion


    #region Defender
    public double DefenderMinimumDefence { get; set; }
    public double DefenderMaximumDefence { get; set; }
    public string DefenderToolsLostJson { get; set; } = "{}";
    #endregion
}

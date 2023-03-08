namespace Chaos.Models.DbModels;

#nullable disable
public class AfterActionReport
{
    public int Id { get; set; }
    public string AggressorId { get; set; }
    public string DefenderId { get; set; }

    #region Aggressor
    public int AggressorRecruitLosses { get; set; }
    public int AggressorAttackerLosses { get; set; }
    #endregion

    #region Defender
    public int DefenderCoinLosses { get; set; }
    public int DefenderRecruitLosses { get; set; }
    public int DefenderDefenderLosses { get; set; }
    #endregion
}

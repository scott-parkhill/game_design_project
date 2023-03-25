using Chaos.Business;
using Chaos.Models.DbModels;
using Chaos.Data;
using Chaos.Models.ViewModels;
using Chaos.Models;

namespace Chaos.Services;

public interface IGameDbService
{
    #region Armies

    /// <summary> Add an army to a user, if one doesn't exist already for that user. </summary>
    Task<DbResult> AddArmy(string loggedUserId);

    /// <summary> Get the army view model for a given user, should it exist. </summary>
    Task<ArmyViewModel?> GetArmyViewModel(string userId);

    /// <summary> This method updates all recruitment and coin generation stuff and is very hacky. </summary>
    Task<DbResult> UpdateArmies();

    /// <summary> Train recruits within an army. </summary>
    Task<DbResult> TrainRecruits(string loggedUserId, int newAttackers, int newDefenders, int newSentries, int newSappers);

    /// <summary> Update the UserWeapons property with the new list of weapons, and remove the cost of the new weapons from the user's gold. </summary>
    Task<DbResult> UpdateUserWeapons(string loggedUserId, params (int Delta, bool IsPurchasing, WeaponTypes WeaponType)[] weapons);
    #endregion

    #region GameUsers
    
    /// <summary> Get the faction for a given user. </summary>
    Task<Factions> GetUserFaction(string loggedUserId);
    #endregion
}

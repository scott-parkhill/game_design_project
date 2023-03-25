using Chaos.Business;
using Chaos.Models;

public static class UserWeaponsDataExtenions
{
    public static IEnumerable<(WeaponTypes WeaponType, Weapon Weapon)> GetWeaponsByActionType(this UserWeaponsData userWeaponsData, ActionTypes actionType)
    {
        List<(WeaponTypes WeaponType, Weapon Weapon)> weapons = userWeaponsData.UserWeapons.Select(u => (u.WeaponType, Weapon.Weapons[u.WeaponType])).ToList();
        
        foreach (var weapon in weapons)
        {
            if (weapon.Weapon.ActionType == actionType)
                yield return weapon;
        }
    }
}

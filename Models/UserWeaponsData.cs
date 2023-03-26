using Chaos.Business;

namespace Chaos.Models;

public class UserWeaponsData
{
    public List<UserWeapon> UserWeapons { get; set; } = new();

    public IEnumerable<(WeaponTypes WeaponType, Weapon Weapon, int Count)> GetWeaponsByActionType(ActionTypes actionType)
    {
        List<(WeaponTypes WeaponType, Weapon Weapon, int Count)> weapons = UserWeapons.Select(u => (u.WeaponType, Weapon.Weapons[u.WeaponType], u.Count)).ToList();
        
        foreach (var weapon in weapons)
        {
            if (weapon.Weapon.ActionType == actionType)
                yield return weapon;
        }
    }
}

public class UserWeapon
{
    public int Count { get; set; }
    public WeaponTypes WeaponType { get; init; }

    public UserWeapon() {}
    public UserWeapon(int count, WeaponTypes weaponType) => (Count, WeaponType) = (count, weaponType);
}

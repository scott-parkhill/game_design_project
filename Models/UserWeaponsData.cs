namespace Chaos.Models;

public class UserWeaponsData
{
    public List<UserWeapon> UserWeapons { get; set; } = new();
}

public class UserWeapon
{
    public int Count { get; set; }
    public WeaponTypes WeaponType { get; init; }

    public UserWeapon(int count, WeaponTypes weaponType) => (Count, WeaponType) = (count, weaponType);
}

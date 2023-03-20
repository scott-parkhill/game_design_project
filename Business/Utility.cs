namespace Chaos.Business;

/// <summary> An abstract weapon that can be used for attack, defense, spying, etc. </summary>
public record Weapon(string Name, string DolphinName, ActionTypes ActionType, int Cost, double Weighting)
{
    /// <summary> Gets the weapon's name based on what faction the user is in. This is to display to the user. </summary>
    public string GetDisplayName(Factions faction) => faction switch
    {
        Factions.Dolphins => DolphinName,
        _ => Name
    };
}

public class Utility
{
    public const string NotFound = "/not-found";
    public const string NotAuthorized = "/not-authorized";

    /// <summary> Dictionary containing all the available weapons in the game, keyed to WeaponTypes. </summary>
    public static readonly Dictionary<WeaponTypes, Weapon> Weapons = new()
    {
        { WeaponTypes.BlackPowderCannon, new("Black Powder Cannon", "Monkfish Launcher", ActionTypes.Offense, 10_000, 20.5) }
    };

    /// <summary> Get an enumerable of Weapons of a specific ActionType. </summary>
    public static IEnumerable<Weapon> GetWeaponsByActionType(ActionTypes actionType)
    {
        foreach (var (_, value) in Weapons)
        {
            if (value.ActionType == actionType)
                yield return value;
        }
    }
}

public enum Factions
{
    None,
    Dolphins,
    Pirates
};

public enum TaskResults
{
    Success,
    Failure,
    Invalid,
};

public enum WeaponTypes
{
    BlackPowderCannon
};

public enum ActionTypes
{
    Offense,
    Defense,
    Recon,
    Sentry
};

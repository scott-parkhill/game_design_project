using Chaos.Business;
using System.Collections.Immutable;

namespace Chaos.Models;

/// <summary> An abstract weapon that can be used for attack, defense, spying, etc. </summary>
public record Weapon(string Name, string DolphinName, ActionTypes ActionType, int Cost, double Strength)
{
    /// <summary> Gets the weapon's name based on what faction the user is in. This is to display to the user. </summary>
    public string GetDisplayName(Factions faction) => faction switch
    {
        Factions.Dolphins => DolphinName,
        _ => Name
    };

    /// <summary> Dictionary containing all the available weapons in the game, keyed to WeaponTypes. </summary>
    public static readonly ImmutableDictionary<WeaponTypes, Weapon> Weapons = new Dictionary<WeaponTypes, Weapon>()
    {
        //Offensive Weapons
        { WeaponTypes.ButterKnife, new("Butter Knife", "Clam Shell", ActionTypes.Offense, 1000, 100)},
        { WeaponTypes.Poignard, new("Poignard", "Coral Shank", ActionTypes.Offense, 4800, 500)},
        { WeaponTypes.Cutlass, new("Cutlass", "Barracuda", ActionTypes.Offense, 10_000, 1100)},
        { WeaponTypes.Schooner, new("Schooner", "Marlin", ActionTypes.Offense, 65_000, 7000)},
        { WeaponTypes.DavyJonesCutlass, new("Davy Jones' Cutlass", "Triden of Atlantis", ActionTypes.Offense, 260_000, 29_000)},
        { WeaponTypes.Frigate, new("Frigate", "Killer Whale", ActionTypes.Offense, 500_000, 60_000)},
        { WeaponTypes.BlackPowderCannon, new("Black Powder Cannon", "Monkfish Launcher", ActionTypes.Offense, 1_100_000, 125_000)},
        { WeaponTypes.ManOWar, new("Man O' War", "Kraken", ActionTypes.Offense, 2_100_000, 260_000)},

        //Defensive Weapons
        { WeaponTypes.LeatherCap, new("Leather Cap", "Coral Helm", ActionTypes.Defense, 1000, 100)},
        { WeaponTypes.Parrot, new("Parrot", "Ocean Sunfih", ActionTypes.Defense, 4800, 500)},
        { WeaponTypes.StackedLeatherArmour, new("Stacked Leather Armour", "Coral Armour", ActionTypes.Defense, 20_000, 2200)},
        { WeaponTypes.SpanishPlateArmour, new("Spanish Plate Armour", "Sea Turtle Armour", ActionTypes.Defense, 65_000, 7000)},
        { WeaponTypes.ReinforcedHullArmour, new("Reinforced Hull Armour", "Shark Skin", ActionTypes.Defense, 260_000, 29_000)},
        { WeaponTypes.BlackbeardsTunic, new("Blackbeard's Tunic", "Sea Urchin Mail", ActionTypes.Defense, 500_000, 60_000)},
        { WeaponTypes.ShipCamouflage, new("Ship Camouflage", "Octopus Camouflage", ActionTypes.Defense, 1_100_000, 125_000)},
        { WeaponTypes.OceanFort, new("Ocean Fort", "Great Barrier Reef", ActionTypes.Defense, 2_100_000, 250_000)},

        //Sapper Tools
        { WeaponTypes.DivingWeight, new("Diving Weight", "Barnacles", ActionTypes.Sapping, 40_000, 4000)},
        { WeaponTypes.SeaweedSuit, new("Seaweed Suit", "Wooden Shell Patterns", ActionTypes.Sapping, 140_000, 15_000)},
        { WeaponTypes.DivingRope, new("Diving Rope", "Crab Chain", ActionTypes.Sapping, 260_000, 29_000)},
        { WeaponTypes.DivingBell, new("Diving Bell", "Crab Catapult", ActionTypes.Sapping, 500_000, 60_000)},
        { WeaponTypes.DivingSpear, new("Diving Spear", "Larger Pincers", ActionTypes.Sapping, 1_100_000, 125_000)},
        { WeaponTypes.DivingSuit, new("Diving Suit", "Hermit Crabs", ActionTypes.Sapping, 2_100_000, 250_000)},

        //Sentry Tools

        {WeaponTypes.WhaleOilLamps, new("Whale Oil Lamps", "Bioluminescent Krill", ActionTypes.Sentry, 40_000, 4000)},
        {WeaponTypes.AlarmHorn, new("Alarm Horn", "Sonar Blast", ActionTypes.Sentry, 140_000, 15_000)},
        {WeaponTypes.Spyglass, new("Spyglass", "Clownfish Lookouts", ActionTypes.Sentry, 260_000, 29_000)},
        {WeaponTypes.Lookout, new("Lookout", "Siren", ActionTypes.Sentry, 500_000, 60_000)},
        {WeaponTypes.CrowsNest, new("Crow's Nest", "Jellyfish Bloom", ActionTypes.Sentry, 1_100_000, 125_000)},
        {WeaponTypes.SeeThroughHullSection, new("See-Through Hull Section", "Blue Whale Patrol", ActionTypes.Sentry, 2_100_000, 250_000)},

    }.ToImmutableDictionary();

    /// <summary> Get an enumerable of Weapons of a specific ActionType. </summary>
    public static IEnumerable<(WeaponTypes WeaponType, Weapon Weapon)> GetWeaponsByActionType(ActionTypes actionType)
    {
        foreach (var (key, value) in Weapons)
        {
            if (value.ActionType == actionType)
                yield return (key, value);
        }
    }
}

public enum WeaponTypes
{
    ButterKnife,
    Poignard,
    Cutlass,
    Schooner,
    DavyJonesCutlass,
    Frigate,
    BlackPowderCannon,
    ManOWar,
    LeatherCap,
    Parrot,
    StackedLeatherArmour,
    SpanishPlateArmour,
    ReinforcedHullArmour,
    BlackbeardsTunic,
    ShipCamouflage,
    OceanFort,
    DivingWeight,
    SeaweedSuit,
    DivingRope,
    DivingBell,
    DivingSpear,
    DivingSuit,
    WhaleOilLamps,
    AlarmHorn,
    Spyglass,
    Lookout,
    CrowsNest,
    SeeThroughHullSection
};

using Chaos.Models;

namespace Chaos.Business;

public static class ArmyScore{

    /// <summary> Get single value representing overall army score. </summary>
    public static double GetArmyScore(UserWeaponsData userWeapons, double[]? modifiers = null) => AllScores(userWeapons, modifiers).Sum();

    /// <summary> Format for the modifiers array: [attack, defence, sentry, sapper] (same as ActionTypes). </summary>
    public static double[] AllScores(UserWeaponsData userWeapons, double[]? modifiers = null){
        modifiers ??= new double[] { 1,1,1,1 };
        return new double[]{
            AttackScore(userWeapons, modifiers[(int)ActionTypes.Offense]),
            DefenceScore(userWeapons, modifiers[(int)ActionTypes.Defence]),
            SentryScore(userWeapons, modifiers[(int)ActionTypes.Sentry]),
            SapperScore(userWeapons, modifiers[(int)ActionTypes.Sapping])
        };
    }

    //formula is (weapon count) * (weapon strenghth) * (normalization value);

    //modifier is the normalization value that will be used to apply the buffs or debuffs
    private static double AttackScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Offense);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private static double DefenceScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Defence);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private static double SentryScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Sentry);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private static double SapperScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Sapping);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }
}

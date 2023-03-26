using Chaos.Models;
using Chaos.Models.ViewModels;

namespace Chaos.Business;

public static class ArmyScore{
    const double RecruitBaseRate = 1;
    const double TrainedBaseRate = 2;

    /// <summary> Get single value representing overall army score. </summary>
    public static double GetArmyScore(ArmyViewModel army, double[]? modifiers = null) => GetArmyScores(army, modifiers).Sum();

    /// <summary> Format for the modifiers array: [attack, defence, sentry, sapper] (same as ActionTypes). </summary>
    public static double[] GetArmyScores(ArmyViewModel army, double[]? modifiers = null){
        modifiers ??= new double[] { 1,1,1,1 };
        return new double[]{
            AttackScore(army, modifiers[(int)ActionTypes.Offence]),
            DefenceScore(army, modifiers[(int)ActionTypes.Defence]),
            SentryScore(army, modifiers[(int)ActionTypes.Sentry]),
            SapperScore(army, modifiers[(int)ActionTypes.Sapping])
        };
    }

    //formula is (weapon count) * (weapon strenghth) * (normalization value);

    //modifier is the normalization value that will be used to apply the buffs or debuffs
    private static double AttackScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Attackers * TrainedBaseRate + army.Recruits * RecruitBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Offence);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }

    private static double DefenceScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Defenders * TrainedBaseRate + army.Recruits * RecruitBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Defence);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }

    private static double SentryScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Sentries * TrainedBaseRate + army.Recruits * RecruitBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Sentry);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + modifier) * modifier;
    }

    private static double SapperScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Sappers * TrainedBaseRate + army.Recruits * RecruitBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Sapping);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + modifier) * modifier;
    }
}

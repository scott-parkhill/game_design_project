using Chaos.Models;
using Chaos.Models.ViewModels;

namespace Chaos.Business;

public static class ArmyScore{
    const double RecruitBaseRate = 1;
    const double TrainedBaseRate = 2;
    const int RecruitIndex = 4;

    /// <summary> Get single value representing overall army score. </summary>
    public static double GetArmyScore(ArmyViewModel army, Factions faction) => GetArmyScores(army, faction).Sum();

    public static double GetOffensiveScore(ArmyViewModel army, Factions faction)
    {
        var scores = GetArmyScores(army, faction);
        return scores[(int)ActionTypes.Offence] + scores[RecruitIndex];
    }
    
    public static double GetDefensiveScore(ArmyViewModel army, Factions faction)
    {
        var scores = GetArmyScores(army, faction);
        return scores[(int)ActionTypes.Defence] + scores[RecruitIndex];
    }
    
    public static double GetSapperScore(ArmyViewModel army, Factions faction)
    {
        var scores = GetArmyScores(army, faction);
        return scores[(int)ActionTypes.Sapping] + scores[RecruitIndex];
    }
    
    public static double GetSentryScore(ArmyViewModel army, Factions faction)
    {
        var scores = GetArmyScores(army, faction);
        return scores[(int)ActionTypes.Sentry] + scores[RecruitIndex];
    }

    /// <summary> Format for the modifiers array: [attack, defence, sentry, sapper] (same as ActionTypes). </summary>
    static double[] GetArmyScores(ArmyViewModel army, Factions faction)
    {
        double[] modifiers = faction switch
        {
            Factions.Pirates => new[] { 1.05, 1.05, 1, 1 },
            Factions.Dolphins => new[] { 1, 1, 1.05, 1.05 },
            _ => new[] { 1.0, 1, 1, 1 }
        };

        var recruitChange = army.Recruits * RecruitBaseRate;

        return new double[]{
            AttackScore(army, modifiers[(int)ActionTypes.Offence]),
            DefenceScore(army, modifiers[(int)ActionTypes.Defence]),
            SentryScore(army, modifiers[(int)ActionTypes.Sentry]),
            SapperScore(army, modifiers[(int)ActionTypes.Sapping]),
            recruitChange
        };
    }

    //formula is (weapon count) * (weapon strength) * (normalization value);

    //modifier is the normalization value that will be used to apply the buffs or debuffs
    private static double AttackScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Attackers * TrainedBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Offence);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }

    private static double DefenceScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Defenders * TrainedBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Defence);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }

    private static double SentryScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Sentries * TrainedBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Sentry);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }

    private static double SapperScore(ArmyViewModel army, double modifier){
        double score = 0;

        var unitModifier = army.Sappers * TrainedBaseRate;
        var values = army.UserWeapons.GetWeaponsByActionType(Business.ActionTypes.Sapping);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return (score + unitModifier) * modifier;
    }
}

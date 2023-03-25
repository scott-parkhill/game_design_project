namespace Chaos.Models;

public class ArmyScore{

    //format for the modifiers array: [attack, defence, sapper, sentry]
    public double[] AllScores(UserWeaponsData userWeapons, double[] modifiers){
        return new double[]{
            AttackScore(userWeapons, modifiers[0]),
            DefenceScore(userWeapons, modifiers[1]),
            SapperScore(userWeapons, modifiers[2]),
            SentryScore(userWeapons, modifiers[3])
        };
    }

    //formula is (weapon count) * (weapon strenghth) * (normalization value);

    //modifier is the normalization value that will be used to apply the buffs or debuffs
    private double AttackScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Offense);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private double DefenceScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Defense);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private double SentryScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Sentry);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

    private double SapperScore(UserWeaponsData weaponsData, double modifier){
        double score = 0;

        var values = weaponsData.GetWeaponsByActionType(Business.ActionTypes.Sapping);

        foreach(var item in values){
            score += item.Count * item.Weapon.Strength;
        }

        return score * modifier;
    }

}
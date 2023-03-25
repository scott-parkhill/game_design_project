
namespace Chaos.Business;

public class BattleCalculator{

    //Method to get the battle outlook percentage and accompanying string for the scouting report.
    public (double, string) getBattleOutlook(double attackerScore, double defenderScore){ 

        double buffNeeded = defenderScore / attackerScore;
        double chanceToWin = 1 - (buffNeeded / 2.0);

        return (chanceToWin, getOutlookString(chanceToWin));
    }

    //Method that gets the string accompanying a certain percentage of success from scouting outlook
    private string getOutlookString(double battleOutlook) => battleOutlook switch
    {
        0 => "No Chance of Victory",
        > 0 and <= 0.1 => "Near Certain Defeat",
        > 0.1 and <= 0.3 => "Highly Likely Defeat",
        > 0.3 and <= 0.4 => "Likely Defeat",
        > 0.4 and <= 0.5 => "Slightly Unlikely Victory",
        > 0.5 and <= 0.6 => "Slightly Likely Victory",
        > 0.6 and <= 0.7 => "Likely Victory",
        > 0.7 and <= 0.9 => "Highly Likely Victory",
        > 0.9 and <= 1 => "Near Certain Victory",
        _ => throw new ArgumentException("Invalid battle outlook")
    };

    //Method for calculating the outcome of a battle.
    public (double, string) calculateBattleOutcome(double attackerScore, double defenderScore){ 
        double randomValue = Utility.Rng.NextDouble() * 2; //generates random double between 0 and 2

        double outcome = (attackerScore * randomValue) / defenderScore;

        if(outcome < 1){ //if the outcome is negative, defender's have won
            return (outcome, "Defender Victory");
        }
        else if(outcome == 1){ //if, in the unlikely scenario the values are tied, flip a coin to decide the winner
            int coinflip = Utility.Rng.Next(0, 2);
            if(coinflip == 0){ //defender wins if a 0 is rolled
                return (outcome, "Defender Victory");
            }
            else{ //attacker wins if a 1 is rolled
                return (outcome, "Attacker Victory");
            }
        }
        else{ //if the score is positive, attackers win the battle
            return (outcome, "Attacker Victory");
        }
    }
}
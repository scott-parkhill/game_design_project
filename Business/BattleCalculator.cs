
namespace Chaos.Business;

public class BattleCalculator{

    //Method to get the battle outlook percentage and accompanying string for the scouting report.
    public (double, string) GetBattleOutlook(double attackerScore, double defenderScore){ 

        double buffNeeded = defenderScore / attackerScore;
        double chanceToWin = 1 - (buffNeeded / 2.0);

        return (chanceToWin, GetOutlookString(chanceToWin));
    }

    //Method that gets the string accompanying a certain percentage of success from scouting outlook
    private string GetOutlookString(double battleOutlook) => battleOutlook switch
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
    public (double, string) CalculateBattleOutcome(double attackerScore, double defenderScore){ 
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

    //Method that returns the variance in the defence values after a sapping/recon operation is conducted
    public ((double, double), string) CalculateSapperOutcome(double sapperScore, double sentryScore, double defenceScore){
        double ratio = sapperScore / sentryScore;
        double min = 0, max = 0;
        string resultMessage = "";

        switch(ratio){ //ratio between the sapper score and the sentry score is used to determine the degree of success or failure of the intel gathering operation
            case var _ when ratio > 2:
                min = defenceScore - (defenceScore * 0.05);
                max = defenceScore + (defenceScore * 0.05);
                resultMessage = "Exceptional Success";
                break;
            case var _ when ratio is >= 1.75 and < 2:
                min = defenceScore - (defenceScore * 0.1);
                max = defenceScore + (defenceScore * 0.1);
                resultMessage = "Great Success";
                break;
            case var _ when ratio is >= 1.5 and < 75:
                min = defenceScore - (defenceScore * 0.15);
                max = defenceScore + (defenceScore * 0.15);
                resultMessage = "Moderate Success";
                break;
            case var _ when ratio is >= 1.25 and < 5:
                min = defenceScore - (defenceScore * 0.2);
                max = defenceScore + (defenceScore * 0.2);
                resultMessage = "Small Success";
                break;
            case var _ when ratio is >= 1 and < 1.25:
                min = defenceScore - (defenceScore * 0.25);
                max = defenceScore + (defenceScore * 0.25);
                resultMessage = "Narrow Success";
                break;
            case var _ when ratio is >= 0.75 and < 1:
                min = defenceScore - (defenceScore * 0.5);
                max = defenceScore + (defenceScore * 0.5);
                resultMessage = "Failure";
                break;
            case var _ when ratio is >= 0.5 and < 0.75:
                min = defenceScore - (defenceScore * 0.75);
                max = defenceScore + (defenceScore * 0.75);
                resultMessage = "Significant Failure";
                break;
            case var _ when ratio < 0.5: //function returns (0, 0) here so we need to remember to either present something like (0, inf) or "No Information" to the user on the results page
                resultMessage = "Catastrophic Failure";
                break;  
        }

        return ((min, max), resultMessage);
    }
}
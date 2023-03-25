namespace Chaos.Business;

public class Utility
{
    public const string NotFound = "/not-found";
    public const string NotAuthorized = "/not-authorized";
    public static readonly Random Rng = new((int)DateTime.UtcNow.TimeOfDay.TotalMicroseconds);
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

public enum ActionTypes
{
    Offense,
    Defense,
    Sentry,
    Sapping
};

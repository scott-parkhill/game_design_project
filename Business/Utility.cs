namespace Chaos.Business;

public class Utility
{
    public const string NotFound = "/not-found";
    public const string NotAuthorized = "/not-authorized";
    public static readonly Random Rng = new((int)DateTime.UtcNow.TimeOfDay.TotalMicroseconds);

    public const string DolphinCurrency = "Pearls";
    public const string PirateCurrency = "Pieces o' Eight";
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
    Offence,
    Defence,
    Sentry,
    Sapping
};

public enum ReportTypes
{
    None,
    AfterActionReport,
    SpyReport
};

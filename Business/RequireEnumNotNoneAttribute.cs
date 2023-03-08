using System.ComponentModel.DataAnnotations;

namespace Chaos.Business;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class RequireEnumNotNoneAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return false;

        Type type = value.GetType();

        if (!type.IsSubclassOf(typeof(Enum)))
            throw new ArgumentException("Cannot use an enum attribute on a non-enum type.");

        Enum.TryParse(type, "None", out object? underlyingNone);

        // No "None" exists on that enum, so there is no fail state.
        if (underlyingNone is null)
            return true;

        // The given value is "None".
        if ((int)underlyingNone == (int)value)
            return false;

        // Otherwise, the enum given is valid and is not "None".
        return true;
    }
}

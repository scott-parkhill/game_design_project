using System.ComponentModel.DataAnnotations;

namespace Chaos.Business;

public class RequiredNotNegativeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string inputNumber)
        {
            var result = Int32.TryParse(inputNumber, out int number);

            if (result is false)
                return false;

            return number >= 0;
        }
        else if (value is int number)
            return number >= 0;
        else
            return false;
    }
}

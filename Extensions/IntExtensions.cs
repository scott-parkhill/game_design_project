using System.Text;

namespace Chaos.Extensions;

public static class IntExtensions
{
    public static string PrettyPrintNumber(this int number)
    {
        StringBuilder sb = new();
        string numberAsString = number.ToString();
        numberAsString = new(numberAsString.Reverse().ToArray());

        for (int i = 0; i < numberAsString.Length;)
        {
            sb.Append(numberAsString[i++]);

            if (i < numberAsString.Length)
                sb.Append(numberAsString[i++]);

            if (i < numberAsString.Length)
                sb.Append(numberAsString[i]);

            if (i++ < numberAsString.Length - 1)
                sb.Append(',');
        }

        return new(sb.ToString().Reverse().ToArray());
    }
}

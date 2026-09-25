namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to English words.
/// </summary>
public class EnglishNumberWords : INumberLanguage
{
    private readonly string[] _ones =
    {
        "zero","one","two","three","four","five","six","seven","eight","nine",
        "ten","eleven","twelve","thirteen","fourteen","fifteen","sixteen","seventeen","eighteen","nineteen"
    };

    private readonly string[] _tens =
    {
        "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1_000_000_000_000, "trillion"),
        (1_000_000_000, "billion"),
        (1_000_000, "million"),
        (1_000, "thousand"),
        (100, "hundred")
    };

    private const string NegativePrefix = "minus";

    public string ConvertInteger(long number)
    {
        if (number == 0) return "zero";
        if (number < 0) return NegativePrefix + " " + ConvertInteger(-number);

        var parts = new List<string>();

        foreach (var (value, name) in _units)
        {
            if (number >= value)
            {
                long unitValue = number / value;
                number %= value;

                if (value == 100)
                {
                    parts.Add($"{_ones[unitValue]} hundred");
                }
                else
                {
                    parts.Add($"{ConvertInteger(unitValue)} {name}");
                }
            }
        }

        if (number > 0)
        {
            if (number < 20)
            {
                parts.Add(_ones[number]);
            }
            else
            {
                long tens = number / 10;
                long ones = number % 10;

                if (tens > 0)
                {
                    string word = _tens[tens];
                    if (ones > 0)
                        word += "-" + _ones[ones];
                    parts.Add(word);
                }
                else if (ones > 0)
                {
                    parts.Add(_ones[ones]);
                }
            }
        }

        return string.Join(" ", parts);
    }

    public string ConvertFractional(int number)
    {
        if (number == 0) return "";
        if (number < 20)
            return _ones[number];
        if (number < 100)
        {
            int tens = number / 10;
            int ones = number % 10;
            if (ones == 0)
                return _tens[tens];
            else
                return $"{_tens[tens]}-{_ones[ones]}";
        }
        return number.ToString();
    }

    public string AddCurrency(string text)
    {
        if (text.EndsWith("dollars") || text.EndsWith("cents"))
            return text;

        return $"{text} dollars";
    }

    public string GetDecimalConnector() => "point";
    public string GetFractionalCurrency() => "cents";
    public string GetNegativePrefix() => NegativePrefix;
}
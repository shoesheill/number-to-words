namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to German words.
/// </summary>
public class GermanNumberWords : INumberLanguage
{
    private readonly Dictionary<int, string> _numbers = new()
    {
        {0, "null"}, {1, "eins"}, {2, "zwei"}, {3, "drei"}, {4, "vier"},
        {5, "fünf"}, {6, "sechs"}, {7, "sieben"}, {8, "acht"}, {9, "neun"},
        {10, "zehn"}, {11, "elf"}, {12, "zwölf"}, {13, "dreizehn"}, {14, "vierzehn"},
        {15, "fünfzehn"}, {16, "sechzehn"}, {17, "siebzehn"}, {18, "achtzehn"}, {19, "neunzehn"},
        {20, "zwanzig"}, {30, "dreißig"}, {40, "vierzig"}, {50, "fünfzig"}, {60, "sechzig"},
        {70, "siebzig"}, {80, "achtzig"}, {90, "neunzig"}
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1000000000000, "Billion"),
        (1000000000, "Milliarde"),
        (1000000, "Million"),
        (1000, "tausend"),
        (100, "hundert")
    };

    private const string NegativePrefix = "minus";

    public string ConvertInteger(long number)
    {
        if (number == 0) return _numbers[0];
        if (number < 0) return NegativePrefix + " " + ConvertInteger(-number);

        var parts = new List<string>();

        foreach (var (value, name) in _units)
        {
            if (number >= value)
            {
                long unitValue = number / value;
                number %= value;

                string prefix = "";
                if (unitValue > 1 || value >= 1000)
                    prefix = ConvertInteger(unitValue);

                parts.Add($"{prefix}{name}".Trim());
            }
        }

        if (number > 0)
        {
            if (_numbers.ContainsKey((int)number))
            {
                parts.Add(_numbers[(int)number]);
            }
            else if (number < 100)
            {
                int tens = (int)(number / 10) * 10;
                int ones = (int)(number % 10);

                string text = "";
                if (ones > 0)
                    text += _numbers[ones] + "und";
                if (tens > 0)
                    text += _numbers[tens];

                parts.Add(text);
            }
        }

        return string.Join(" ", parts);
    }

    public string ConvertFractional(int number)
    {
        if (number == 0) return "";
        if (_numbers.ContainsKey(number))
            return _numbers[number];
        return ConvertInteger(number);
    }

    public string AddCurrency(string text)
    {
        if (text.EndsWith("Euro") || text.EndsWith("Cent"))
            return text;

        return $"{text} Euro";
    }

    public string GetDecimalConnector() => "Komma";
    public string GetFractionalCurrency() => "Cent";
    public string GetNegativePrefix() => NegativePrefix;
}
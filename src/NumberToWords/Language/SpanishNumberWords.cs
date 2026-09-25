namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to Spanish words.
/// </summary>
public class SpanishNumberWords : INumberLanguage
{
    private readonly string[] _ones =
    {
        "cero","uno","dos","tres","cuatro","cinco","seis","siete","ocho","nueve",
        "diez","once","doce","trece","catorce","quince","dieciséis","diecisiete","dieciocho","diecinueve"
    };

    private readonly string[] _tens =
    {
        "", "", "veinte","treinta","cuarenta","cincuenta","sesenta","setenta","ochenta","noventa"
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1_000_000_000_000, "billón"),
        (1_000_000_000, "mil millones"),
        (1_000_000, "millón"),
        (1_000, "mil"),
        (100, "cien")
    };

    private const string NegativePrefix = "menos";

    public string ConvertInteger(long number)
    {
        if (number == 0) return "cero";
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
                    parts.Add($"{_ones[unitValue]} cien");
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
        if (text.EndsWith("euros") || text.EndsWith("céntimos"))
            return text;

        return $"{text} euros";
    }

    public string GetDecimalConnector() => "coma";
    public string GetFractionalCurrency() => "céntimos";
    public string GetNegativePrefix() => NegativePrefix;
}
namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to Japanese words.
/// </summary>
public class JapaneseNumberWords : INumberLanguage
{
    private readonly string[] _numbers = new string[]
    {
        "零", "一", "二", "三", "四", "五", "六", "七", "八", "九",
        "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九"
    };

    private readonly string[] _tens = new string[]
    {
        "", "", "二十", "三十", "四十", "五十", "六十", "七十", "八十", "九十"
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1000000000000, "兆"),
        (100000000, "億"),
        (10000, "万"),
        (1000, "千"),
        (100, "百"),
        (10, "十")
    };

    private const string NegativePrefix = "マイナス";

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
                if (value >= 1000 && unitValue == 1) prefix = "";
                else prefix = ConvertInteger(unitValue);

                parts.Add($"{prefix}{name}".Trim());
            }
        }

        if (number > 0)
        {
            if (number < 20)
                parts.Add(_numbers[number]);
            else
            {
                int tens = (int)(number / 10);
                int ones = (int)(number % 10);

                string text = _tens[tens];
                if (ones > 0)
                    text += _numbers[ones];

                parts.Add(text);
            }
        }

        return string.Join("", parts);
    }

    public string ConvertFractional(int number)
    {
        if (number == 0) return "";
        return ConvertInteger(number);
    }

    public string AddCurrency(string text)
    {
        if (text.EndsWith("円") || text.EndsWith("銭"))
            return text;

        return $"{text}円";
    }

    public string GetDecimalConnector() => "点";
    public string GetFractionalCurrency() => "銭";
    public string GetNegativePrefix() => NegativePrefix;
}
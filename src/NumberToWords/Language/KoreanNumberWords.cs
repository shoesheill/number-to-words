namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to Korean words.
/// </summary>
public class KoreanNumberWords : INumberLanguage
{
    private readonly string[] _numbers = new string[]
    {
        "영", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구",
        "십", "십일", "십이", "십삼", "십사", "십오", "십육", "십칠", "십팔", "십구"
    };

    private readonly string[] _tens = new string[]
    {
        "", "", "이십", "삼십", "사십", "오십", "육십", "칠십", "팔십", "구십"
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1000000000000, "조"),
        (100000000, "억"),
        (10000, "만"),
        (1000, "천"),
        (100, "백"),
        (10, "십")
    };

    private const string NegativePrefix = "마이너스";

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
                if (unitValue == 1 && value < 10000) prefix = "";
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
        if (text.EndsWith("원") || text.EndsWith("전"))
            return text;

        return $"{text}원";
    }

    public string GetDecimalConnector() => "점";
    public string GetFractionalCurrency() => "전";
    public string GetNegativePrefix() => NegativePrefix;
}
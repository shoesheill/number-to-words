namespace NumberToWords.Language;

/// <summary>
/// Converts numbers to Vietnamese words.
/// </summary>
public class VietnameseNumberWords : INumberLanguage
{
    private readonly string[] _numbers = new string[]
    {
        "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín",
        "mười", "mười một", "mười hai", "mười ba", "mười bốn", "mười lăm",
        "mười sáu", "mười bảy", "mười tám", "mười chín"
    };

    private readonly string[] _tens = new string[]
    {
        "", "", "hai mươi", "ba mươi", "bốn mươi", "năm mươi",
        "sáu mươi", "bảy mươi", "tám mươi", "chín mươi"
    };

    private readonly (long, string)[] _units = new (long, string)[]
    {
        (1000000000, "tỷ"),
        (1000000, "triệu"),
        (1000, "nghìn"),
        (100, "trăm"),
        (10, "mươi")
    };

    private const string NegativePrefix = "âm";

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

                parts.Add($"{prefix}{(prefix == "" ? "" : " ")}{name}".Trim());
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
                    text += " " + _numbers[ones];

                parts.Add(text);
            }
        }

        return string.Join(" ", parts);
    }

    public string ConvertFractional(int number)
    {
        if (number == 0) return "";
        if (number < _numbers.Length)
            return _numbers[number];
        return ConvertInteger(number);
    }

    public string AddCurrency(string text)
    {
        if (text.EndsWith("đồng") || text.EndsWith("hào"))
            return text;

        return $"{text} đồng";
    }

    public string GetDecimalConnector() => "phẩy";
    public string GetFractionalCurrency() => "hào";
    public string GetNegativePrefix() => NegativePrefix;
}
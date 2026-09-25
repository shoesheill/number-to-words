using System.Collections.Generic;

namespace NumberToWords.Converter;

/// <summary>
/// Provides direct dictionary-based conversion for languages that have explicit word entries for every number up to 99.
/// </summary>
public static class DirectConverter
{
    public static string ConvertInteger(long number, Dictionary<int, string> _numbers, (long, string)[] _units, string negativePrefix, bool isUnitOptional = false)
    {
        if (number == 0) return _numbers[0];
        if (number < 0) return negativePrefix + " " + ConvertInteger(-number, _numbers, _units, negativePrefix, isUnitOptional);

        var parts = new List<string>();

        foreach (var (value, name) in _units)
        {
            if (number >= value)
            {
                long unitValue = number / value;
                number %= value;
                if (!isUnitOptional || unitValue > 1 || value >= 1000)
                    parts.Add($"{ConvertInteger(unitValue, _numbers, _units, negativePrefix, isUnitOptional)} {name}");
                else
                    parts.Add(name);
            }
        }

        if (number > 0)
        {
            if (_numbers.ContainsKey((int)number))
                parts.Add(_numbers[(int)number]);
            else if (number < 100)
            {
                int tens = (int)(number / 10) * 10;
                int ones = (int)(number % 10);
                if (tens > 0 && _numbers.ContainsKey(tens))
                    parts.Add(_numbers[tens]);
                if (ones > 0 && _numbers.ContainsKey(ones))
                    parts.Add(_numbers[ones]);
            }
        }

        return string.Join(" ", parts);
    }

    public static string ConvertFractional(int number, Dictionary<int, string> _numbers, (long, string)[] _units, string negativePrefix, bool isUnitOptional = false)
    {
        if (number == 0) return "";
        if (_numbers.ContainsKey(number))
            return _numbers[number];
        return ConvertInteger(number, _numbers, _units, negativePrefix, isUnitOptional);
    }

    internal static string ConvertInteger(long number, Dictionary<int, string> numbers, (long, string)[] units)
    {
        throw new NotImplementedException();
    }

}
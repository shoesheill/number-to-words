using NumberToWords.Enum;

namespace NumberToWords.Services;

/// <summary>
/// Default implementation of <see cref="INumberToWordsService"/>.
/// </summary>
public class NumberToWordsService : INumberToWordsService
{
    /// <summary>
    /// Converts a decimal number to its word representation in the specified language.
    /// </summary>
    public string Convert(decimal number, LanguageEnum language, bool includeCurrency = false)
    {
        var converter = NumberLanguageFactory.GetLanguageConverter(language);

        decimal roundedNumber = Math.Round(number, 2);
        long integerPart = (long)Math.Truncate(roundedNumber);
        int fractionPart = (int)Math.Abs(Math.Round((roundedNumber - integerPart) * 100));

        string result = converter.ConvertInteger(integerPart);

        if (fractionPart > 0)
        {
            string fractionalText = converter.ConvertFractional(fractionPart);
            if (includeCurrency)
            {
                string fractionalCurrency = converter.GetFractionalCurrency();
                result = $"{converter.AddCurrency(result)} {fractionalText} {fractionalCurrency}";
            }
            else
            {
                result += $" {converter.GetDecimalConnector()} {fractionalText}";
            }
        }
        else if (includeCurrency)
        {
            result = converter.AddCurrency(result);
        }

        return result.Trim();
    }
}
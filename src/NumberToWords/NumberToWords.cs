using NumberToWords.Enum;
using NumberToWords.Services;

namespace NumberToWords;

/// <summary>
/// Static facade for converting numbers to words across multiple languages.
/// </summary>
public static class NumberToWords
{
    private static readonly INumberToWordsService _service = new NumberToWordsService();

    /// <summary>
    /// Converts a decimal number to its word representation in the specified language.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <param name="language">The target language.</param>
    /// <param name="includeCurrency">When true, appends currency names to integer and fractional parts.</param>
    /// <returns>The word representation of the number.</returns>
    public static string Convert(decimal number, LanguageEnum language, bool includeCurrency = false)
    {
        return _service.Convert(number, language, includeCurrency);
    }
}

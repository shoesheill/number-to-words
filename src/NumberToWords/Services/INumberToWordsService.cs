using NumberToWords.Enum;

namespace NumberToWords.Services;

/// <summary>
/// Provides conversion of numbers to their word representation in multiple languages.
/// </summary>
public interface INumberToWordsService
{
    /// <summary>
    /// Converts a decimal number to its word representation in the specified language.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <param name="language">The target language.</param>
    /// <param name="includeCurrency">When true, appends currency names to integer and fractional parts.</param>
    /// <returns>The word representation of the number.</returns>
    string Convert(decimal number, LanguageEnum language, bool includeCurrency = false);
}
namespace NumberToWords.Language;

/// <summary>
/// Defines the contract for language-specific number-to-words conversion.
/// </summary>
public interface INumberLanguage
{
    /// <summary>Converts a long integer to its word representation.</summary>
    string ConvertInteger(long number);

    /// <summary>Converts a fractional part (0-99) to its word representation.</summary>
    string ConvertFractional(int number);

    /// <summary>Appends the currency name to the given text.</summary>
    string AddCurrency(string text);

    /// <summary>Returns the language-specific word for the decimal separator.</summary>
    string GetDecimalConnector();

    /// <summary>Returns the language-specific word for the fractional currency unit.</summary>
    string GetFractionalCurrency();

    /// <summary>Returns the language-specific prefix for negative numbers (e.g. "minus", "moins").</summary>
    string GetNegativePrefix();
}
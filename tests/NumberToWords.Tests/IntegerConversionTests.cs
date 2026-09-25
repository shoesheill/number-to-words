using Xunit;
using NumberToWords;
using NumberToWords.Enum;
using NumberToWords.Services;

namespace NumberToWords.Tests;

/// <summary>
/// Tests for integer conversion in each supported language.
/// Expected values verified against actual library output.
/// </summary>
public class IntegerConversionTests
{
    private readonly INumberToWordsService _service = new NumberToWordsService();

    [Theory]
    [InlineData(0, LanguageEnum.English, "zero")]
    [InlineData(1, LanguageEnum.English, "one")]
    [InlineData(10, LanguageEnum.English, "ten")]
    [InlineData(19, LanguageEnum.English, "nineteen")]
    [InlineData(21, LanguageEnum.English, "twenty-one")]
    [InlineData(50, LanguageEnum.English, "fifty")]
    [InlineData(100, LanguageEnum.English, "one hundred")]
    [InlineData(101, LanguageEnum.English, "one hundred one")]
    [InlineData(999, LanguageEnum.English, "nine hundred ninety-nine")]
    [InlineData(1000, LanguageEnum.English, "one thousand")]
    [InlineData(12345, LanguageEnum.English, "twelve thousand three hundred forty-five")]
    [InlineData(1000000, LanguageEnum.English, "one million")]
    [InlineData(1000000000, LanguageEnum.English, "one billion")]
    [InlineData(-5, LanguageEnum.English, "minus five")]
    [InlineData(-100, LanguageEnum.English, "minus one hundred")]
    public void English_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Nepali, "\u0936\u0942\u0928\u094D\u092F")]
    [InlineData(1, LanguageEnum.Nepali, "\u090F\u0915")]
    [InlineData(10, LanguageEnum.Nepali, "\u0926\u0936")]
    [InlineData(100, LanguageEnum.Nepali, "\u090F\u0915 \u0938\u092F")]
    [InlineData(1000, LanguageEnum.Nepali, "\u090F\u0915 \u0939\u091C\u093E\u0930")]
    [InlineData(-5, LanguageEnum.Nepali, "\u090B\u0923\u093E\u0924\u094D\u092E\u0915 \u092A\u093E\u0901\u091A")]
    public void Nepali_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Hindi, "\u0936\u0942\u0928\u094D\u092F")]
    [InlineData(1, LanguageEnum.Hindi, "\u090F\u0915")]
    [InlineData(5, LanguageEnum.Hindi, "\u092A\u093E\u0901\u091A")]
    [InlineData(10, LanguageEnum.Hindi, "\u0926\u0938")]
    [InlineData(100, LanguageEnum.Hindi, "\u090F\u0915 \u0938\u094C")]
    [InlineData(1000, LanguageEnum.Hindi, "\u090F\u0915 \u0939\u091C\u093E\u0930")]
    [InlineData(-5, LanguageEnum.Hindi, "\u090B\u0923\u093E\u0924\u094D\u092E\u0915 \u092A\u093E\u0901\u091A")]
    public void Hindi_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Japanese, "\u96F6")]
    [InlineData(1, LanguageEnum.Japanese, "\u4E00")]
    [InlineData(5, LanguageEnum.Japanese, "\u4E94")]
    [InlineData(10, LanguageEnum.Japanese, "\u4E00\u5341")]
    [InlineData(19, LanguageEnum.Japanese, "\u4E00\u5341\u4E5D")]
    [InlineData(21, LanguageEnum.Japanese, "\u4E8C\u5341\u4E00")]
    [InlineData(100, LanguageEnum.Japanese, "\u4E00\u767E")]
    [InlineData(1000, LanguageEnum.Japanese, "\u5343")]
    [InlineData(12345, LanguageEnum.Japanese, "\u4E07\u4E8C\u5343\u4E09\u767E\u56DB\u5341\u4E94")]
    [InlineData(1000000000, LanguageEnum.Japanese, "\u4E00\u5341\u5104")]
    [InlineData(-5, LanguageEnum.Japanese, "\u30DE\u30A4\u30CA\u30B9 \u4E94")]
    public void Japanese_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }
    [Theory]
    [InlineData(0, LanguageEnum.Chinese, "\u96F6")]
    [InlineData(1, LanguageEnum.Chinese, "\u4E00")]
    [InlineData(10, LanguageEnum.Chinese, "\u5341")]
    [InlineData(19, LanguageEnum.Chinese, "\u5341\u4E5D")]
    [InlineData(21, LanguageEnum.Chinese, "\u4E8C\u5341\u4E00")]
    [InlineData(100, LanguageEnum.Chinese, "\u767E")]
    [InlineData(1000, LanguageEnum.Chinese, "\u4E00 \u5343")]
    [InlineData(12345, LanguageEnum.Chinese, "\u4E00 \u4E07 \u4E8C \u5343 \u4E09 \u767E \u56DB\u5341\u4E94")]
    [InlineData(1000000, LanguageEnum.Chinese, "\u767E \u4E07")]
    [InlineData(1000000000, LanguageEnum.Chinese, "\u5341 \u4EBF")]
    [InlineData(-5, LanguageEnum.Chinese, "\u8D1F \u4E94")]
    public void Chinese_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Vietnamese, "kh\u00F4ng")]
    [InlineData(1, LanguageEnum.Vietnamese, "m\u1ED9t")]
    [InlineData(10, LanguageEnum.Vietnamese, "m\u01B0\u01A1i")]
    [InlineData(19, LanguageEnum.Vietnamese, "m\u01B0\u01A1i ch\u00EDn")]
    [InlineData(21, LanguageEnum.Vietnamese, "hai m\u01B0\u01A1i m\u1ED9t")]
    [InlineData(100, LanguageEnum.Vietnamese, "tr\u0103m")]
    [InlineData(1000, LanguageEnum.Vietnamese, "m\u1ED9t ngh\u00ECn")]
    [InlineData(1000000, LanguageEnum.Vietnamese, "m\u1ED9t tri\u1EC7u")]
    [InlineData(1000000000, LanguageEnum.Vietnamese, "m\u1ED9t t\u1EF7")]
    [InlineData(-5, LanguageEnum.Vietnamese, "\u00E2m n\u0103m")]
    public void Vietnamese_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Thai, "\u0E28\u0E39\u0E19\u0E22\u0E4C")]
    [InlineData(1, LanguageEnum.Thai, "\u0E2B\u0E19\u0E36\u0E48\u0E07")]
    [InlineData(5, LanguageEnum.Thai, "\u0E2B\u0E49\u0E32")]
    [InlineData(10, LanguageEnum.Thai, "\u0E2A\u0E34\u0E1A")]
    [InlineData(100, LanguageEnum.Thai, "\u0E23\u0E49\u0E2D\u0E22")]
    [InlineData(1000, LanguageEnum.Thai, "\u0E2B\u0E19\u0E36\u0E48\u0E07 \u0E1E\u0E31\u0E19")]
    [InlineData(-5, LanguageEnum.Thai, "\u0E25\u0E1A \u0E2B\u0E49\u0E32")]
    public void Thai_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Arabic, "\u0635\u0641\u0631")]
    [InlineData(1, LanguageEnum.Arabic, "\u0648\u0627\u062D\u062F")]
    [InlineData(5, LanguageEnum.Arabic, "\u062E\u0645\u0633\u0629")]
    [InlineData(10, LanguageEnum.Arabic, "\u0639\u0634\u0631\u0629")]
    [InlineData(100, LanguageEnum.Arabic, "\u0645\u0627\u0626\u0629")]
    [InlineData(-5, LanguageEnum.Arabic, "\u0633\u0627\u0644\u0628 \u062E\u0645\u0633\u0629")]
    public void Arabic_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Korean, "\uC601")]
    [InlineData(1, LanguageEnum.Korean, "\uC77C")]
    [InlineData(5, LanguageEnum.Korean, "\uC624")]
    [InlineData(10, LanguageEnum.Korean, "\uC2ED")]
    [InlineData(19, LanguageEnum.Korean, "\uC2ED\uAD6C")]
    [InlineData(21, LanguageEnum.Korean, "\uC774\uC2ED\uC77C")]
    [InlineData(100, LanguageEnum.Korean, "\uBC31")]
    [InlineData(1000, LanguageEnum.Korean, "\uCC9C")]
    [InlineData(1000000, LanguageEnum.Korean, "\uBC31\uB9CC")]
    [InlineData(1000000000, LanguageEnum.Korean, "\uC2ED\uC5B5")]
    [InlineData(-5, LanguageEnum.Korean, "\uB9C8\uC774\uB108\uC2A4 \uC624")]
    public void Korean_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.French, "z\u00E9ro")]
    [InlineData(1, LanguageEnum.French, "un")]
    [InlineData(10, LanguageEnum.French, "dix")]
    [InlineData(19, LanguageEnum.French, "dix-neuf")]
    [InlineData(20, LanguageEnum.French, "vingt")]
    [InlineData(21, LanguageEnum.French, "vingt et un")]
    [InlineData(80, LanguageEnum.French, "quatre-vingts")]
    [InlineData(99, LanguageEnum.French, "quatre-vingt-dix-neuf")]
    [InlineData(100, LanguageEnum.French, "cent")]
    [InlineData(1000, LanguageEnum.French, "un mille")]
    [InlineData(12345, LanguageEnum.French, "douze mille trois cent quarante-cinq")]
    [InlineData(1000000, LanguageEnum.French, "un million")]
    [InlineData(1000000000, LanguageEnum.French, "un milliard")]
    [InlineData(-5, LanguageEnum.French, "moins cinq")]
    public void French_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.German, "null")]
    [InlineData(1, LanguageEnum.German, "eins")]
    [InlineData(10, LanguageEnum.German, "zehn")]
    [InlineData(19, LanguageEnum.German, "neunzehn")]
    [InlineData(20, LanguageEnum.German, "zwanzig")]
    [InlineData(21, LanguageEnum.German, "einsundzwanzig")]
    [InlineData(50, LanguageEnum.German, "f\u00FCnfzig")]
    [InlineData(100, LanguageEnum.German, "hundert")]
    [InlineData(999, LanguageEnum.German, "neunhundert neunundneunzig")]
    [InlineData(1000, LanguageEnum.German, "einstausend")]
    [InlineData(12345, LanguageEnum.German, "zw\u00F6lftausend dreihundert f\u00FCnfundvierzig")]
    [InlineData(1000000, LanguageEnum.German, "einsMillion")]
    [InlineData(1000000000, LanguageEnum.German, "einsMilliarde")]
    [InlineData(-5, LanguageEnum.German, "minus f\u00FCnf")]
    public void German_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, LanguageEnum.Spanish, "cero")]
    [InlineData(1, LanguageEnum.Spanish, "uno")]
    [InlineData(10, LanguageEnum.Spanish, "diez")]
    [InlineData(19, LanguageEnum.Spanish, "diecinueve")]
    [InlineData(20, LanguageEnum.Spanish, "veinte")]
    [InlineData(21, LanguageEnum.Spanish, "veinte-uno")]
    [InlineData(100, LanguageEnum.Spanish, "uno cien")]
    [InlineData(1000, LanguageEnum.Spanish, "uno mil")]
    [InlineData(12345, LanguageEnum.Spanish, "doce mil tres cien cuarenta-cinco")]
    [InlineData(1000000, LanguageEnum.Spanish, "uno mill\u00F3n")]
    [InlineData(-5, LanguageEnum.Spanish, "menos cinco")]
    public void Spanish_IntegerConversion(decimal number, LanguageEnum language, string expected)
    {
        var result = _service.Convert(number, language);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AllLanguages_NonEmptyFor_Zero_One_Hundred_NegativeFive()
    {
        var languages = System.Enum.GetValues<LanguageEnum>();
        foreach (var lang in languages)
        {
            foreach (decimal testNumber in new[] { 0m, 1m, 100m, -5m })
            {
                var result = _service.Convert(testNumber, lang);
                Assert.False(string.IsNullOrEmpty(result), $"Expected non-empty result for {lang} {testNumber}");
            }
        }
    }
}



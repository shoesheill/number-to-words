using System.Collections.Concurrent;
using NumberToWords.Enum;
using NumberToWords.Language;

namespace NumberToWords.Services;

/// <summary>
/// Factory that creates the appropriate language converter for a given <see cref="LanguageEnum"/>.
/// </summary>
public static class NumberLanguageFactory
{
    /// <summary>
    /// Returns a cached language converter for the specified language.
    /// </summary>
    public static INumberLanguage GetLanguageConverter(LanguageEnum language)
    {
        return language switch
        {
            LanguageEnum.English => GetOrCache(language, () => new EnglishNumberWords()),
            LanguageEnum.Nepali => GetOrCache(language, () => new NepaliNumberWords()),
            LanguageEnum.Hindi => GetOrCache(language, () => new HindiNumberWords()),
            LanguageEnum.Japanese => GetOrCache(language, () => new JapaneseNumberWords()),
            LanguageEnum.Chinese => GetOrCache(language, () => new ChineseNumberWords()),
            LanguageEnum.Vietnamese => GetOrCache(language, () => new VietnameseNumberWords()),
            LanguageEnum.Thai => GetOrCache(language, () => new ThaiNumberWords()),
            LanguageEnum.Arabic => GetOrCache(language, () => new ArabicNumberWords()),
            LanguageEnum.Korean => GetOrCache(language, () => new KoreanNumberWords()),
            LanguageEnum.French => GetOrCache(language, () => new FrenchNumberWords()),
            LanguageEnum.German => GetOrCache(language, () => new GermanNumberWords()),
            LanguageEnum.Spanish => GetOrCache(language, () => new SpanishNumberWords()),
            _ => throw new NotSupportedException($"Language '{language}' is not supported.")
        };
    }
    
    private static readonly ConcurrentDictionary<LanguageEnum, INumberLanguage> _cache = new();
    
    private static INumberLanguage GetOrCache(LanguageEnum language, Func<INumberLanguage> factory)
    {
        return _cache.GetOrAdd(language, _ => factory());
    }
}
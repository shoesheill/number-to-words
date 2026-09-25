# NumberToWords for .NET

[![NuGet](https://img.shields.io/badge/NuGet-NumberToWords.Core-blue)](https://www.nuget.org/packages/NumberToWords.Core/)
[![Targets](https://img.shields.io/badge/targets-.NET%206%20%7C%20.NET%208%20%7C%20.NET%2010-purple)](https://learn.microsoft.com/dotnet/)

Convert numbers to words in 12 languages with a small, dependency-free .NET library.

## Capabilities

- Convert positive, negative, integer, and decimal values.
- Convert values from 0 through large whole-number scales supported by the language converters.
- Round decimal input to two places.
- Add language-specific decimal connectors.
- Optionally append language-specific currency names and fractional currency names.
- Use the same conversion engine from a static facade or through the service interface.
- Target `net6.0`, `net8.0`, and `net10.0`.
- Includes XML documentation and a NuGet symbol package.

![NumberToWords capabilities](capabilities.svg)

## Install

```bash
dotnet add package NumberToWords.Core
```

## Quick start

```csharp
using NumberToWords.Enum;
using NumberToWordsFacade = NumberToWords.NumberToWords;

string english = NumberToWordsFacade.Convert(12345, LanguageEnum.English);
// "twelve thousand three hundred forty-five"

string french = NumberToWordsFacade.Convert(12345, LanguageEnum.French);
// "douze mille trois cent quarante-cinq"
```

## Decimal and currency examples

```csharp
using NumberToWords.Enum;
using NumberToWordsFacade = NumberToWords.NumberToWords;

string decimalWords = NumberToWordsFacade.Convert(12.34m, LanguageEnum.English);
// "twelve point thirty-four"

string currencyWords = NumberToWordsFacade.Convert(
    12.34m,
    LanguageEnum.English,
    includeCurrency: true);
// "twelve dollars thirty-four cents"
```

## Service API

For dependency injection or testability, use `INumberToWordsService`:

```csharp
using NumberToWords;
using NumberToWords.Enum;
using NumberToWords.Services;

INumberToWordsService service = new NumberToWordsService();
string result = service.Convert(1000, LanguageEnum.Japanese);
```

## Supported languages

| Code | Language |
| --- | --- |
| `English` | English |
| `Nepali` | Nepali |
| `Hindi` | Hindi |
| `Japanese` | Japanese |
| `Chinese` | Chinese |
| `Vietnamese` | Vietnamese |
| `Thai` | Thai |
| `Arabic` | Arabic |
| `Korean` | Korean |
| `French` | French |
| `German` | German |
| `Spanish` | Spanish |

## All languages at a glance

The table below uses the same input for every language: `12345` for the integer output and `12.34` with currency enabled for the second output.

| Language | `12345` | `12.34` with currency |
| --- | --- | --- |
| English | twelve thousand three hundred forty-five | twelve dollars thirty-four cents |
| Nepali | बाह्र हजार तीन सय पैंचालीस | बाह्र रुपैया चौंतीस पैसा |
| Hindi | बारह हजार तीन सौ पैंतालीस | बारह रुपया चौंतीस पैसे |
| Japanese | 万二千三百四十五 | 一十二円 三十四 銭 |
| Chinese | 一 万 二 千 三 百 四十五 | 十二元 三十四 分 |
| Vietnamese | mươi hai nghìn ba trăm bốn mươi năm | mươi hai đồng ba mươi bốn hào |
| Thai | หนึ่ง หมื่น สอง พัน สาม ร้อย สี่สิบห้า | สิบสอง บาท สามสิบสี่ สตางค์ |
| Arabic | اثنا عشر ألف ثلاثة مائة خمسة وأربعون | اثنا عشر ريال أربعة وثلاثون هللة |
| Korean | 일만이천삼백사십오 | 십이원 삼십사 전 |
| French | douze mille trois cent quarante-cinq | douze euro trente-quatre centime |
| German | zwölftausend dreihundert fünfundvierzig | zwölf Euro vierunddreißig Cent |
| Spanish | doce mil tres cien cuarenta-cinco | doce euros treinta y cuatro céntimos |

## API summary

| API | Purpose |
| --- | --- |
| `NumberToWords.Convert(...)` | Main static conversion method |
| `INumberToWordsService.Convert(...)` | Service-based conversion |
| `LanguageEnum` | Supported language selection |
| `INumberLanguage` | Language converter contract |

## Build from source

```bash
dotnet restore D:\Repo\NumberToWords\NumberToWords.sln
dotnet test D:\Repo\NumberToWords\NumberToWords.sln --configuration Release
dotnet pack D:\Repo\NumberToWords\src\NumberToWords\NumberToWords.csproj --configuration Release
```

## License

MIT. See `LICENSE` in the repository root.

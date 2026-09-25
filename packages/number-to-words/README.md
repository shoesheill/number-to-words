# number-to-word

Convert numbers to words in 12 languages from Node.js and other JavaScript runtimes. The package is dependency-free, CommonJS-compatible, and includes TypeScript declarations.

[![npm](https://img.shields.io/badge/npm-number--to--word-red)](https://www.npmjs.com/package/number-to-word)
[![Node.js](https://img.shields.io/badge/Node.js-18%2B-green)](https://nodejs.org/)

![NumberToWords capabilities](capabilities.svg)

## Capabilities

- Convert positive and negative integers.
- Convert decimal values rounded to two places.
- Add decimal connectors in the selected language.
- Optionally include currency and fractional currency names.
- Use short language codes or full language names.
- Work in CommonJS applications and TypeScript projects through `index.d.ts`.
- Run without runtime dependencies.

## Install

```bash
npm install number-to-word
```

## Quick start

```js
const { convert } = require('number-to-word');

convert(12345);
// "twelve thousand three hundred forty-five"

convert(12345, 'fr');
// "douze mille trois cent quarante-cinq"

convert(12345, 'ja');
// "一万二千三百四十五"
```

## Decimals and currency

```js
convert(12.34);
// "twelve point thirty-four"

convert(12.34, 'en', true);
// "twelve dollars thirty-four cents"

convert(12.34, { language: 'de', includeCurrency: true });
// "zwölf Euro dreißig vier Cent"
```

## API

### `convert(number, language?, includeCurrency?)`

Converts a finite JavaScript number to words. The language can be a short code, a full language name, or an options object.

```js
const { convert } = require('number-to-word');

convert(42);                              // "forty-two"
convert(42, 'Spanish');                   // "cuarenta-dos"
convert(42.5, { language: 'English' });  // "forty-two point five"
```

### `convertInteger(number, language?)`

Converts a safe integer only.

```js
const { convertInteger } = require('number-to-word');

convertInteger(1000, 'th');
// "หนึ่ง พัน"
```

### `convertFractional(number, language?)`

Converts an integer from 0 through 99. Zero returns an empty string, matching fractional conversion behavior.

```js
const { convertFractional } = require('number-to-word');

convertFractional(34);
// "thirty-four"
```

### `languages`

Returns the supported language codes.

```js
const { languages } = require('number-to-word');

console.log(languages);
// ['en', 'ne', 'hi', 'ja', 'zh', 'vi', 'th', 'ar', 'ko', 'fr', 'de', 'es']
```

## Supported languages

| Code | Language |
| --- | --- |
| `en` | English |
| `ne` | Nepali |
| `hi` | Hindi |
| `ja` | Japanese |
| `zh` | Chinese |
| `vi` | Vietnamese |
| `th` | Thai |
| `ar` | Arabic |
| `ko` | Korean |
| `fr` | French |
| `de` | German |
| `es` | Spanish |

Full names such as `English`, `French`, `Japanese`, and `Vietnamese` are accepted as aliases.

## All languages at a glance

The table below uses `12345` for the integer output and `12.34` with currency enabled for the second output.

| Language | `12345` | `12.34` with currency |
| --- | --- | --- |
| English (`en`) | twelve thousand three hundred forty-five | twelve dollars thirty-four cents |
| Nepali (`ne`) | बाह्र हजार तीन सय पैंचालीस | बाह्र रुपैया चौंतीस पैसा |
| Hindi (`hi`) | बारह हजार तीन सौ पैंतालीस | बारह रुपया चौंतीस पैसे |
| Japanese (`ja`) | 万二千三百四十五 | 一十二 円 三十四 銭 |
| Chinese (`zh`) | 一 万 二 千 三 百 四十五 | 十二 元 三十四 分 |
| Vietnamese (`vi`) | mười hai nghìn ba trăm bốn mươi năm | mười hai đồng ba mươi bốn hào |
| Thai (`th`) | หนึ่ง หมื่น สอง พัน สาม ร้อย สี่สิบห้า | สิบสอง บาท สามสิบสี่ สตางค์ |
| Arabic (`ar`) | اثنا عشر ألف ثلاثة مائة خمسة وأربعون | اثنا عشر ريال أربعة وثلاثون هللة |
| Korean (`ko`) |만이천삼백사십오 | 십이 원 삼십사 전 |
| French (`fr`) | douze mille trois cent quarante-cinq | douze euro trente-quatre centime |
| German (`de`) | zwölftausend dreihundert fünfundvierzig | zwölf Euro vierunddreißig Cent |
| Spanish (`es`) | doce mil tres cien cuarenta-cinco | doce euros treinta y cuatro céntimos |

## Development

```bash
npm ci
npm test
```

## License

MIT. See `LICENSE`.

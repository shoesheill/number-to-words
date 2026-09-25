'use strict';

const languageCodes = Object.freeze([
  'en', 'ne', 'hi', 'ja', 'zh', 'vi', 'th', 'ar', 'ko', 'fr', 'de', 'es',
]);

const aliases = new Map([
  ['en', 'en'], ['english', 'en'],
  ['ne', 'ne'], ['nepali', 'ne'],
  ['hi', 'hi'], ['hindi', 'hi'],
  ['ja', 'ja'], ['japanese', 'ja'],
  ['zh', 'zh'], ['chinese', 'zh'],
  ['vi', 'vi'], ['vietnamese', 'vi'],
  ['th', 'th'], ['thai', 'th'],
  ['ar', 'ar'], ['arabic', 'ar'],
  ['ko', 'ko'], ['korean', 'ko'],
  ['fr', 'fr'], ['french', 'fr'],
  ['de', 'de'], ['german', 'de'],
  ['es', 'es'], ['spanish', 'es'],
]);

const englishOnes = [
  'zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine',
  'ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen',
  'seventeen', 'eighteen', 'nineteen',
];
const englishTens = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
const englishUnits = [
  [1_000_000_000_000, 'trillion'],
  [1_000_000_000, 'billion'],
  [1_000_000, 'million'],
  [1_000, 'thousand'],
  [100, 'hundred'],
];

function makeChineseWords() {
  const digits = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九'];
  return Array.from({ length: 100 }, (_, number) => {
    if (number < 10) return digits[number];
    if (number < 20) return `十${digits[number % 10]}`;
    if (number % 10 === 0) return `${digits[Math.floor(number / 10)]}十`;
    return `${digits[Math.floor(number / 10)]}十${digits[number % 10]}`;
  });
}

function makeArabicWords() {
  const words = [
    'صفر', 'واحد', 'اثنان', 'ثلاثة', 'أربعة', 'خمسة', 'ستة', 'سبعة', 'ثمانية', 'تسعة',
    'عشرة', 'أحد عشر', 'اثنا عشر', 'ثلاثة عشر', 'أربعة عشر', 'خمسة عشر', 'ستة عشر',
    'سبعة عشر', 'ثمانية عشر', 'تسعة عشر',
  ];
  const tens = ['', '', 'عشرون', 'ثلاثون', 'أربعون', 'خمسون', 'ستون', 'سبعون', 'ثمانون', 'تسعون'];
  for (let number = 20; number < 100; number += 1) {
    const tensValue = tens[Math.floor(number / 10)];
    const ones = words[number % 10];
    words[number] = number % 10 === 0 ? tensValue : `${ones} و${tensValue}`;
  }
  return words;
}

function makeFrenchWords() {
  const words = [
    'zéro', 'un', 'deux', 'trois', 'quatre', 'cinq', 'six', 'sept', 'huit', 'neuf',
    'dix', 'onze', 'douze', 'treize', 'quatorze', 'quinze', 'seize', 'dix-sept',
    'dix-huit', 'dix-neuf',
  ];
  const tens = {
    2: 'vingt', 3: 'trente', 4: 'quarante', 5: 'cinquante', 6: 'soixante',
    7: 'soixante', 8: 'quatre-vingt', 9: 'quatre-vingt',
  };
  for (let number = 20; number < 100; number += 1) {
    const tensValue = tens[Math.floor(number / 10)];
    const ones = number % 10;
    if (number === 71) {
      words[number] = 'soixante et onze';
    } else if (number >= 70 && number < 80) {
      words[number] = number === 71 ? 'soixante et onze' : `soixante-${words[number - 60]}`;
    } else if (number >= 80) {
      words[number] = number === 80 ? 'quatre-vingts' : `quatre-vingt-${words[number - 80]}`;
    } else if (ones === 1) {
      words[number] = `${tensValue} et un`;
    } else {
      words[number] = ones === 0 ? tensValue : `${tensValue}-${words[ones]}`;
    }
  }
  return words;
}

function makeThaiWords() {
  const words = [
    'ศูนย์', 'หนึ่ง', 'สอง', 'สาม', 'สี่', 'ห้า', 'หก', 'เจ็ด', 'แปด', 'เก้า',
    'สิบ', 'สิบเอ็ด', 'สิบสอง', 'สิบสาม', 'สิบสี่', 'สิบห้า', 'สิบหก', 'สิบเจ็ด', 'สิบแปด', 'สิบเก้า',
  ];
  const tens = ['', '', 'ยี่สิบ', 'สามสิบ', 'สี่สิบ', 'ห้าสิบ', 'หกสิบ', 'เจ็ดสิบ', 'แปดสิบ', 'เก้าสิบ'];
  for (let number = 20; number < 100; number += 1) {
    words[number] = number % 10 === 0
      ? tens[Math.floor(number / 10)]
      : `${tens[Math.floor(number / 10)]}${words[number % 10]}`;
  }
  return words;
}

function makeSouthAsianWords(ones, teens, tens) {
  const words = [...ones, ...teens];
  for (let number = 20; number < 100; number += 1) {
    const tensValue = tens[Math.floor(number / 10)];
    const one = ones[number % 10];
    words[number] = number % 10 === 0 ? tensValue : `${tensValue} ${one}`;
  }
  return words;
}

function applyCompoundWords(words, values) {
  let index = 0;
  for (let number = 21; number < 100; number += 1) {
    if (number % 10 !== 0) words[number] = values[index++];
  }
}

const hindiWords = makeSouthAsianWords(
  ['शून्य', 'एक', 'दो', 'तीन', 'चार', 'पाँच', 'छह', 'सात', 'आठ', 'नौ'],
  ['दस', 'ग्यारह', 'बारह', 'तेरह', 'चौदह', 'पंद्रह', 'सोलह', 'सत्रह', 'अठारह', 'उन्नीस'],
  ['', '', 'बीस', 'तीस', 'चालीस', 'पचास', 'साठ', 'सत्तर', 'अस्सी', 'नब्बे'],
);
const nepaliWords = makeSouthAsianWords(
  ['शून्य', 'एक', 'दुई', 'तीन', 'चार', 'पाँच', 'छ', 'सात', 'आठ', 'नौ'],
  ['दश', 'एघार', 'बाह्र', 'तेह्र', 'चौध', 'पन्ध्र', 'सोह्र', 'सत्र', 'अठार', 'उन्नाइस'],
  ['', '', 'बीस', 'तीस', 'चालीस', 'पचास', 'साठी', 'सत्तर', 'असी', 'नब्बे'],
);
applyCompoundWords(hindiWords, 'इक्काइस बाईस तेइस चौबीस पच्चीस छब्बीस सत्ताईस अट्ठाईस उनतीस इकतीस बत्तीस तैंतीस चौंतीस पैंतीस छत्तीस सैंतीस अड़तीस उनतालीस इकतालीस बयालीस त्रियालीस चवालीस पैंतालीस छियालीस सैंतालीस अड़तालीस उनचास इक्यावन बावन त्रिपन्न चौवन पचपन छप्पन सत्तावन अठावन उनसठ इकसठ बासठ त्रिसठ चौंसठ पैंसठ छियासठ सड़सठ अठासठ उनहत्तर इकहत्तर बहत्तर तिहत्तर चौहत्तर पचहत्तर छिहत्तर सतहत्तर अठहत्तर उनासी इक्यासी बयासी तिरासी चौरासी पचासी छियासी सतासी अठासी नवासी इक्यानवे बानवे त्रियानवे चौरानवे पचानवे छियानवे सतानवे अठानवे निन्यानवे'.split(' '));
applyCompoundWords(nepaliWords, 'एक्काइस बाइस तेइस चौबिस पच्चिस छब्बिस सत्ताइस अट्ठाइस उनन्तिस एकत्तिस बत्तिस तेत्तिस चौंतीस पैंतीस छत्तिस सैंतीस अठत्तिस उनन्चालीस एकचालीस बयालीस त्रियालीस चवालीस पैंचालीस छयालीस सतचालीस अठचालीस उनन्चास एकाउन्न बाउन्न त्रिपन्न चउन्न पचपन्न छपन्न सन्ताउन्न अन्ठाउन्न उनन्सठी एकसठ्ठी बासठ्ठी त्रिसठ्ठी चौंसठ्ठी पैंसठ्ठी छैसठ्ठी सड्सठ्ठी अठ्सठ्ठी उनन्सत्तरी एकहत्तर बहत्तर त्रिहत्तर चौहत्तर पचहत्तर छिहत्तर सतहत्तर अठहत्तर उनासी एकासी बयासी तिरासी चौरासी पचासी छियासी सतासी अठासी उनान्नब्बे एकान्नब्बे बयान्नब्बे त्रियान्नब्बे चौरान्नब्बे पन्चान्नब्बे छयान्नब्बे सन्तान्नब्बे अन्ठान्नब्बे उनान्सय'.split(' '));

const japaneseDigits = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二', '十三', '十四', '十五', '十六', '十七', '十八', '十九'];
const japaneseTens = ['', '', '二十', '三十', '四十', '五十', '六十', '七十', '八十', '九十'];
const koreanDigits = ['영', '일', '이', '삼', '사', '오', '육', '칠', '팔', '구', '십', '십일', '십이', '십삼', '십사', '십오', '십육', '십칠', '십팔', '십구'];
const koreanTens = ['', '', '이십', '삼십', '사십', '오십', '육십', '칠십', '팔십', '구십'];
const vietnameseDigits = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín', 'mười', 'mười một', 'mười hai', 'mười ba', 'mười bốn', 'mười lăm', 'mười sáu', 'mười bảy', 'mười tám', 'mười chín'];
const vietnameseTens = ['', '', 'hai mươi', 'ba mươi', 'bốn mươi', 'năm mươi', 'sáu mươi', 'bảy mươi', 'tám mươi', 'chín mươi'];

const directLanguages = {
  ne: { words: nepaliWords, units: [[1_000_000_000_000, 'खरब'], [10_000_000_000, 'अरब'], [10_000_000, 'करोड'], [100_000, 'लाख'], [1_000, 'हजार'], [100, 'सय']], negative: 'ऋणात्मक', unitOptional: false, currency: 'रुपैया', fractionCurrency: 'पैसा', connector: 'डशमलव' },
  hi: { words: hindiWords, units: [[10_000_000_000, 'अरब'], [10_000_000, 'करोड़'], [100_000, 'लाख'], [1_000, 'हजार'], [100, 'सौ']], negative: 'ऋणात्मक', unitOptional: false, currency: 'रुपया', fractionCurrency: 'पैसे', connector: 'अंक' },
  zh: { words: makeChineseWords(), units: [[1_000_000_000_000, '兆'], [100_000_000, '亿'], [10_000, '万'], [1_000, '千'], [100, '百']], negative: '负', unitOptional: true, currency: '元', fractionCurrency: '分', connector: '点' },
  fr: { words: makeFrenchWords(), units: [[1_000_000_000_000, 'billion'], [1_000_000_000, 'milliard'], [1_000_000, 'million'], [1_000, 'mille'], [100, 'cent']], negative: 'moins', unitOptional: true, currency: 'euro', fractionCurrency: 'centime', connector: 'virgule' },
  th: { words: makeThaiWords(), units: [[1_000_000, 'ล้าน'], [100_000, 'แสน'], [10_000, 'หมื่น'], [1_000, 'พัน'], [100, 'ร้อย']], negative: 'ลบ', unitOptional: true, currency: 'บาท', fractionCurrency: 'สตางค์', connector: 'จุด' },
  ar: { words: makeArabicWords(), units: [[1_000_000_000_000, 'تريليون'], [1_000_000_000, 'مليار'], [1_000_000, 'مليون'], [1_000, 'ألف'], [100, 'مائة']], negative: 'سالب', unitOptional: true, currency: 'ريال', fractionCurrency: 'هللة', connector: 'فاصل' },
};

function directConvertInteger(number, language) {
  const config = directLanguages[language];
  if (number === 0) return config.words[0];
  if (number < 0) return `${config.negative} ${directConvertInteger(-number, language)}`;
  const parts = [];
  let remaining = number;
  for (const [value, unit] of config.units) {
    if (remaining < value) continue;
    const count = Math.floor(remaining / value);
    remaining %= value;
    const includeCount = !config.unitOptional || count > 1 || value >= 1000;
    parts.push(includeCount ? `${directConvertInteger(count, language)} ${unit}` : unit);
  }
  if (remaining > 0) parts.push(config.words[remaining]);
  return parts.join(' ');
}

function englishConvertInteger(number) {
  if (number === 0) return 'zero';
  if (number < 0) return `minus ${englishConvertInteger(-number)}`;
  const parts = [];
  let remaining = number;
  for (const [value, unit] of englishUnits) {
    if (remaining < value) continue;
    const count = Math.floor(remaining / value);
    remaining %= value;
    parts.push(value === 100 ? `${englishOnes[count]} hundred` : `${englishConvertInteger(count)} ${unit}`);
  }
  if (remaining >= 20) {
    const ones = remaining % 10;
    const word = englishTens[Math.floor(remaining / 10)];
    parts.push(ones ? `${word}-${englishOnes[ones]}` : word);
  } else if (remaining > 0) parts.push(englishOnes[remaining]);
  return parts.join(' ');
}

function germanConvertInteger(number) {
  if (number === 0) return 'null';
  if (number < 0) return `minus ${germanConvertInteger(-number)}`;
  const ones = ['null', 'eins', 'zwei', 'drei', 'vier', 'fünf', 'sechs', 'sieben', 'acht', 'neun'];
  const teens = ['', 'zehn', 'elf', 'zwölf', 'dreizehn', 'vierzehn', 'fünfzehn', 'sechzehn', 'siebzehn', 'achtzehn', 'neunzehn'];
  const tens = ['', '', 'zwanzig', 'dreißig', 'vierzig', 'fünfzig', 'sechzig', 'siebzig', 'achtzig', 'neunzig'];
  const underHundred = (value) => {
    if (value < 10) return ones[value];
    if (value < 20) return teens[value - 9];
    return `${value % 10 ? `${ones[value % 10]}und` : ''}${tens[Math.floor(value / 10)]}`;
  };
  const units = [[1_000_000_000_000, 'Billion'], [1_000_000_000, 'Milliarde'], [1_000_000, 'Million'], [1000, 'tausend'], [100, 'hundert']];
  const parts = [];
  let remaining = number;
  for (const [value, unit] of units) {
    if (remaining < value) continue;
    const count = Math.floor(remaining / value);
    remaining %= value;
    const prefix = count > 1 || value >= 1000 ? germanConvertInteger(count) : '';
    parts.push(`${prefix}${unit}`);
  }
  if (remaining > 0) parts.push(underHundred(remaining));
  return parts.join(' ');
}

function eastAsianConvertInteger(number, language) {
  const digits = language === 'ja' ? japaneseDigits : language === 'ko' ? koreanDigits : vietnameseDigits;
  const tens = language === 'ja' ? japaneseTens : language === 'ko' ? koreanTens : vietnameseTens;
  const negative = language === 'ja' ? 'マイナス' : language === 'ko' ? '마이너스' : 'âm';
  if (number === 0) return digits[0];
  if (number < 0) return `${negative} ${eastAsianConvertInteger(-number, language)}`;
  const units = language === 'ja'
    ? [[1_000_000_000_000, '兆'], [100_000_000, '億'], [10_000, '万'], [1000, '千'], [100, '百'], [10, '十']]
    : language === 'ko'
      ? [[1_000_000_000_000, '조'], [100_000_000, '억'], [10_000, '만'], [1000, '천'], [100, '백'], [10, '십']]
      : [[1_000_000_000, 'tỷ'], [1_000_000, 'triệu'], [1000, 'nghìn'], [100, 'trăm'], [10, 'mười']];
  const parts = [];
  let remaining = number;
  for (const [value, unit] of units) {
    if (remaining < value) continue;
    const count = Math.floor(remaining / value);
    remaining %= value;
    let prefix = '';
    if (language === 'vi') prefix = count > 1 || value >= 1000 ? `${eastAsianConvertInteger(count, language)} ` : '';
    else if (unit === '十') prefix = eastAsianConvertInteger(count, language);
    else if (language === 'ja') prefix = value < 1000 || count > 1 ? eastAsianConvertInteger(count, language) : '';
    else if (language === 'ko') prefix = count > 1 ? eastAsianConvertInteger(count, language) : '';
    parts.push(`${prefix}${unit}`);
  }
  if (remaining > 0) {
    if (remaining < 20) parts.push(digits[remaining]);
    else parts.push(`${tens[Math.floor(remaining / 10)]}${remaining % 10 ? digits[remaining % 10] : ''}`);
  }
  return parts.join(language === 'vi' ? ' ' : '');
}

function spanishConvertInteger(number) {
  if (number === 0) return 'cero';
  if (number < 0) return `menos ${spanishConvertInteger(-number)}`;
  const ones = ['cero', 'uno', 'dos', 'tres', 'cuatro', 'cinco', 'seis', 'siete', 'ocho', 'nueve', 'diez', 'once', 'doce', 'trece', 'catorce', 'quince', 'dieciséis', 'diecisiete', 'dieciocho', 'diecinueve'];
  const tens = ['', '', 'veinte', 'treinta', 'cuarenta', 'cincuenta', 'sesenta', 'setenta', 'ochenta', 'noventa'];
  const units = [[1_000_000_000_000, 'billón'], [1_000_000_000, 'mil millones'], [1_000_000, 'millón'], [1000, 'mil'], [100, 'cien']];
  const parts = [];
  let remaining = number;
  for (const [value, unit] of units) {
    if (remaining < value) continue;
    const count = Math.floor(remaining / value);
    remaining %= value;
    parts.push(value === 100 ? `${ones[count]} ${unit}` : `${spanishConvertInteger(count)} ${unit}`);
  }
  if (remaining >= 20) parts.push(remaining % 10 ? `${tens[Math.floor(remaining / 10)]}-${ones[remaining % 10]}` : tens[Math.floor(remaining / 10)]);
  else if (remaining > 0) parts.push(ones[remaining]);
  return parts.join(' ');
}


function normalizeLanguage(language) {
  const value = String(language ?? 'en').trim().toLowerCase();
  const normalized = aliases.get(value);
  if (!normalized) throw new RangeError(`Unsupported language: ${language}`);
  return normalized;
}

function convertIntegerValue(number, language) {
  if (!Number.isSafeInteger(number)) throw new TypeError('number must be a safe integer');
  if (language === 'en') return englishConvertInteger(number);
  if (language === 'de') return germanConvertInteger(number);
  if (['ja', 'ko', 'vi'].includes(language)) return eastAsianConvertInteger(number, language);
  if (language === 'es') return spanishConvertInteger(number);
  return directConvertInteger(number, language);
}

function convertFractionalValue(number, language) {
  if (!Number.isInteger(number) || number < 0 || number > 99) throw new RangeError('fraction must be an integer between 0 and 99');
  if (number === 0) return '';
  if (language === 'en') return number < 20 ? englishOnes[number] : `${englishTens[Math.floor(number / 10)]}${number % 10 ? `-${englishOnes[number % 10]}` : ''}`;
  if (['ja', 'ko', 'vi'].includes(language)) return convertIntegerValue(number, language);
  if (language === 'de') return germanConvertInteger(number);
  if (language === 'es') return spanishConvertInteger(number);
  return directLanguages[language].words[number] || convertIntegerValue(number, language);
}

function addCurrency(text, language) {
  if (language === 'en') return text.endsWith('dollars') || text.endsWith('cents') ? text : `${text} dollars`;
  if (language === 'de') return text.endsWith('Euro') || text.endsWith('Cent') ? text : `${text} Euro`;
  if (language === 'es') return text.endsWith('euros') || text.endsWith('céntimos') ? text : `${text} euros`;
  if (['ja', 'ko', 'vi'].includes(language)) {
    const currency = language === 'ja' ? '円' : language === 'ko' ? '원' : 'đồng';
    return text.endsWith(currency) ? text : `${text} ${currency}`;
  }
  const config = directLanguages[language];
  return text.endsWith(config.currency) ? text : `${text} ${config.currency}`;
}

function fractionCurrency(language) {
  if (language === 'en') return 'cents';
  if (language === 'de') return 'Cent';
  if (language === 'es') return 'céntimos';
  if (language === 'ja') return '銭';
  if (language === 'ko') return '전';
  if (language === 'vi') return 'hào';
  return directLanguages[language].fractionCurrency;
}

function decimalConnector(language) {
  if (language === 'en') return 'point';
  if (language === 'ja' || language === 'zh') return '点';
  if (language === 'ko') return '점';
  if (language === 'de') return 'Komma';
  if (language === 'es') return 'coma';
  return directLanguages[language].connector;
}

function convert(number, language = 'en', includeCurrency = false) {
  if (typeof language === 'object' && language !== null) {
    includeCurrency = language.includeCurrency ?? false;
    language = language.language ?? 'en';
  }
  if (typeof number !== 'number' || !Number.isFinite(number)) throw new TypeError('number must be a finite number');
  const languageCode = normalizeLanguage(language);
  const rounded = Math.round((number + Number.EPSILON) * 100) / 100;
  let integerPart = Math.trunc(rounded);
  let fractionPart = Math.round(Math.abs((rounded - integerPart) * 100));
  if (fractionPart === 100) {
    integerPart += rounded < 0 ? -1 : 1;
    fractionPart = 0;
  }
  let result = convertIntegerValue(integerPart, languageCode);
  if (fractionPart > 0) {
    const fraction = convertFractionalValue(fractionPart, languageCode);
    result = includeCurrency
      ? `${addCurrency(result, languageCode)} ${fraction} ${fractionCurrency(languageCode)}`
      : `${result} ${decimalConnector(languageCode)} ${fraction}`;
  } else if (includeCurrency) {
    result = addCurrency(result, languageCode);
  }
  return result.trim();
}

module.exports = {
  convert,
  convertInteger: (number, language = 'en') => convertIntegerValue(number, normalizeLanguage(language)),
  convertFractional: (number, language = 'en') => convertFractionalValue(number, normalizeLanguage(language)),
  languages: languageCodes,
};


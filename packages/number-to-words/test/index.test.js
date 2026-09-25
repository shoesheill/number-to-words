const test = require('node:test');
const assert = require('node:assert/strict');
const {
  convert,
  convertInteger,
  convertFractional,
  languages,
} = require('../index');

test('exposes all supported languages', () => {
  assert.deepEqual(languages, ['en', 'ne', 'hi', 'ja', 'zh', 'vi', 'th', 'ar', 'ko', 'fr', 'de', 'es']);
});

test('converts English integers', () => {
  assert.equal(convertInteger(0), 'zero');
  assert.equal(convertInteger(21), 'twenty-one');
  assert.equal(convertInteger(12345), 'twelve thousand three hundred forty-five');
  assert.equal(convertInteger(-100), 'minus one hundred');
});

test('converts representative language integers', () => {
  assert.equal(convertInteger(1000, 'th'), 'หนึ่ง พัน');
  assert.equal(convertInteger(12345, 'fr'), 'douze mille trois cent quarante-cinq');
  assert.equal(convertInteger(1000000, 'ja'), '一百万');
  assert.equal(convertInteger(1000000, 'ko'), '백만');
  assert.equal(convertInteger(1000000, 'vi'), 'một triệu');
  assert.equal(convertInteger(12345, 'es'), 'doce mil tres cien cuarenta-cinco');
});

test('converts decimals and currency output', () => {
  assert.equal(convert(12.34), 'twelve point thirty-four');
  assert.equal(convert(12.34, 'en', true), 'twelve dollars thirty-four cents');
  assert.equal(convert(12, { language: 'de', includeCurrency: true }), 'zwölf Euro');
});

test('converts fractional parts directly', () => {
  assert.equal(convertFractional(0), '');
  assert.equal(convertFractional(34), 'thirty-four');
  assert.equal(convertFractional(99, 'fr'), 'quatre-vingt-dix-neuf');
});

test('converts every supported language without throwing', () => {
  const names = ['English', 'Nepali', 'Hindi', 'Japanese', 'Chinese', 'Vietnamese', 'Thai', 'Arabic', 'Korean', 'French', 'German', 'Spanish'];
  for (const name of names) assert.ok(convert(100, name).length > 0, name);
});

test('accepts full language names', () => {
  assert.equal(convert(1, 'English'), 'one');
  assert.equal(convert(100, 'Japanese'), '一百');
  assert.equal(convert(10, 'Vietnamese'), 'mười');
});

test('rejects unsupported languages and invalid numbers', () => {
  assert.throws(() => convert(1, 'xx'), /Unsupported language/);
  assert.throws(() => convert('1'), /finite number/);
});

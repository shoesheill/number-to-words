export type Language =
  | 'en'
  | 'ne'
  | 'hi'
  | 'ja'
  | 'zh'
  | 'vi'
  | 'th'
  | 'ar'
  | 'ko'
  | 'fr'
  | 'de'
  | 'es';

export type LanguageName =
  | 'English'
  | 'Nepali'
  | 'Hindi'
  | 'Japanese'
  | 'Chinese'
  | 'Vietnamese'
  | 'Thai'
  | 'Arabic'
  | 'Korean'
  | 'French'
  | 'German'
  | 'Spanish';

export type LanguageInput = Language | LanguageName | Lowercase<LanguageName>;

export interface ConvertOptions {
  language?: LanguageInput;
  includeCurrency?: boolean;
}

export declare function convert(
  number: number,
  language?: LanguageInput | ConvertOptions,
  includeCurrency?: boolean,
): string;

export declare function convertInteger(number: number, language?: LanguageInput): string;

export declare function convertFractional(number: number, language?: LanguageInput): string;

export declare const languages: readonly Language[];

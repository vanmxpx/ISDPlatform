import { Languages } from '@enums';

export class LanguagesHelper {

    public static getLanguageKeys(): string[] {
        return Object.keys(Languages).map((key) => Languages[key]);
    }

    public static getLanguageTitles(): string[] {
        return this.getLanguageKeys().map((key) => this.getLanguageTitle(Languages[key]));
    }

    public static getLanguageTitle(language: Languages): string {
        switch (language) {
            case Languages.English: return 'English';
            case Languages.Russian: return 'Русский';
            case Languages.Ukrainian: return 'Українська';
        }
    }
}

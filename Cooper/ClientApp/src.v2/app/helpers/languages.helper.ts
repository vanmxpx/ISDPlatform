import { Languages } from '@enums';

export class LanguagesHelper {

    static getLanguageKeys(): string[] {
        return Object.keys(Languages).map(key => Languages[key]);
    }

    static getLanguageTitles(): string[] {
        return this.getLanguageKeys().map(key => this.getLanguageTitle(Languages[key]));
    }

    static getLanguageTitle(language: Languages): string {
        switch (language) {
            case Languages.English: return 'English';
            case Languages.Russian: return 'Русский';
            case Languages.Ukrainian: return 'Українська';
        }
    }
}

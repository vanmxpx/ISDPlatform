import { Injectable } from '@angular/core';
import { Languages } from '@enums';
import { LanguagesHelper } from '@helpers';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class LocalizationService {
  public DEFAULT_LANGUAGE = Languages.English;
  public helper = LanguagesHelper;
  public languageKeys = this.helper.getLanguageKeys();
  public languages = Languages;

  getCurrentLanguage(): string {
    if (localStorage) {
        return localStorage.getItem('language') || this.DEFAULT_LANGUAGE;
    } else {
        return this.DEFAULT_LANGUAGE;
    }
}

setCurrentLanguage(language: string) {
    if (localStorage) {
        localStorage.setItem('language', language);
    }
}

switchLanguage() {
  this.languageKeys = this.helper.getLanguageKeys();

  this.translate.addLangs(this.languageKeys);
  this.translate.setDefaultLang(this.getCurrentLanguage());

  const browserLang = this.getCurrentLanguage();
  this.translate.currentLang =  browserLang;
  this.translate.use(browserLang);
}

onLanguageChanged(language: string) {
  this.setCurrentLanguage(language);
  this.translate.use(language);
}

  constructor(public translate: TranslateService) {
  }
}

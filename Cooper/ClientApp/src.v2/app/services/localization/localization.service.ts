import { Injectable } from '@angular/core';
import { Languages } from '@enums';
import { LanguagesHelper } from '@helpers';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class LocalizationService {
  public DEFAULT_LANGUAGE: string = Languages.English;
  public helper: any = LanguagesHelper;
  public languageKeys: any = this.helper.getLanguageKeys();
  public languages: any = Languages;

  public getCurrentLanguage(): string {
    if (localStorage) {
        return localStorage.getItem('language') || this.DEFAULT_LANGUAGE;
    } else {
        return this.DEFAULT_LANGUAGE;
    }
}

public setCurrentLanguage(language: string): void {
    if (localStorage) {
        localStorage.setItem('language', language);
    }
}

public switchLanguage(): void {
  this.languageKeys = this.helper.getLanguageKeys();

  this.translate.addLangs(this.languageKeys);
  this.translate.setDefaultLang(this.getCurrentLanguage());

  const browserLang = this.getCurrentLanguage();
  this.translate.currentLang =  browserLang;
  this.translate.use(browserLang);
}

public onLanguageChanged(language: string): void {
  this.setCurrentLanguage(language);
  this.translate.use(language);
}

  constructor(public translate: TranslateService) {
  }
}

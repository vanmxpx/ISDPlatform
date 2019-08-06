import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguagesHelper } from '@helpers';

@Component({
  selector: 'coop-root',
 templateUrl: './app.component.html',
 styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentLanguage = localStorage.getItem('language');
  constructor(public translate: TranslateService) {

    const helper = LanguagesHelper;
    const languageKeys = helper.getLanguageKeys();

    this.translate.addLangs(languageKeys);
    this.translate.setDefaultLang(this.currentLanguage);

    const browserLang = this.currentLanguage;
    this.translate.currentLang =  browserLang;
    this.translate.use(browserLang);
  }
}





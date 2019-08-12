import { Component, OnInit, Input, Output, ViewEncapsulation, EventEmitter } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from '@services';
import { Languages } from 'src.v2/app/enums';

@Component({
  selector: 'coop-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LanguageSelectComponent implements OnInit {
  helper = this.localizationService.helper;
  languageKeys = this.localizationService.languageKeys;

  languages = this.localizationService.languages;
  currentLanguage = this.localizationService.getCurrentLanguage();
  onLangChangeEvent(lang: string) {
    this.localizationService.onLanguageChanged(lang);
  }
  getLanguageTitle(lang: Languages) {
    return this.helper.getLanguageTitle(lang);
  }

  constructor(public translate: TranslateService, private localizationService: LocalizationService) {
    this.translate.setDefaultLang(this.currentLanguage);
  }
  ngOnInit() {
  }
}

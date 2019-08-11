import { Component, OnInit, Input, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from '@services';

@Component({
  selector: 'coop-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss']
})
export class LanguageSelectComponent implements OnInit {
  helper = this.localizationService.helper;
  languageKeys = this.localizationService.languageKeys;

  languages = this.localizationService.languages;
  currentLanguage = this.localizationService.getCurrentLanguage();

  constructor(public translate: TranslateService, private localizationService: LocalizationService) {
    this.translate.setDefaultLang(this.currentLanguage);
  }
  ngOnInit() {
  }
}

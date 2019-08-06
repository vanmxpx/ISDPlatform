import { Component, OnInit } from '@angular/core';
import { Languages } from '@enums';
import { TranslateService } from '@ngx-translate/core';
import { LanguagesHelper } from '@helpers';
import { LanguageStorageService } from '@services';

@Component({
  selector: 'coop-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss']
})
export class LanguageSelectComponent implements OnInit {
  helper = LanguagesHelper;
  languageKeys = this.helper.getLanguageKeys();

  languages = Languages;
  currentLanguage = this.translateService.getLanguage();

  constructor(public translate: TranslateService, private translateService: LanguageStorageService) {
    this.translate.setDefaultLang(this.currentLanguage);
  }

  onLanguageChanged(language: string, translate: TranslateService) {
    this.translateService.setLanguage(language);
    this.translate.use(language);
  }

  ngOnInit() {
  }
}

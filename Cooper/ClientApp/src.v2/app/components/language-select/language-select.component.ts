import { Component, ViewEncapsulation, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from '@services';

@Component({
  selector: 'coop-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LanguageSelectComponent {
  @Input() public languageKeys: string[] = this.localizationService.languageKeys;

  @Input() public languages: any = this.localizationService.languages;
  @Input() public currentLanguage: string = this.localizationService.getCurrentLanguage();
  public onLangChangeEvent(lang: string): void {
    this.localizationService.onLanguageChanged(lang);
  }

  constructor(public translate: TranslateService, private localizationService: LocalizationService) {
    translate.setDefaultLang(this.currentLanguage);
  }
}

import { Component, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from '@services';

@Component({
  selector: 'coop-language-select',
  templateUrl: './language-select.component.html',
  styleUrls: ['./language-select.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LanguageSelectComponent {
  @Input() public languageKeys: string[];
  @Input() public languages: any;
  @Input() public currentLanguage: string;
  @Output() public langChangeEvent: EventEmitter<LocalizationService>;

  constructor(public translate: TranslateService) {
    translate.setDefaultLang(this.currentLanguage);
  }
}

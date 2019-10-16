import { Component, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';

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
  @Output() public langChangeEvent: EventEmitter<string> = new EventEmitter<string>();
  @Output() public settingDefaultLang: EventEmitter<string> = new EventEmitter<string>();

  public onLangChanged(lang: string): void {
    this.langChangeEvent.emit(lang);
  }

  public setDefaultLang(): void {
    this.settingDefaultLang.emit(this.currentLanguage);
  }
}

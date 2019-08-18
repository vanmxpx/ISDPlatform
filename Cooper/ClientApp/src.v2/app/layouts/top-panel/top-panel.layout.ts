import { Component, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CoopNavBarItem } from '@models';
import { LocalizationService } from '@services';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss'],
  encapsulation: ViewEncapsulation.None
})

export class TopPanelLayoutComponent {
  public languageKeys: string[] = this.localizationService.languageKeys;
  public languages: any = this.localizationService.languages;
  public currentLanguage: string = this.localizationService.getCurrentLanguage();
  public navigationItems: CoopNavBarItem[] = [
    {label: 'TOP-PANEL.HOME', link: '#'},
    {label: 'TOP-PANEL.GAMES', link: '/platform/games'},
    {label: 'TOP-PANEL.CHATS', link: '#'},
    {label: 'TOP-PANEL.MY-PROFILE', link: '#'},
    {label: 'TOP-PANEL.FORUM', link: '#'},
    {label: 'TOP-PANEL.VACANCIES', link: '#'}
  ];

  constructor(public translate: TranslateService, public localizationService: LocalizationService) {
   }
   public onLangChangeEvent(lang: string): void {
    this.localizationService.onLanguageChanged(lang);
  }
}

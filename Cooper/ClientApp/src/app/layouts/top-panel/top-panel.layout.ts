import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CoopNavBarItem } from '@models';
import { LocalizationService, SessionService} from '@services';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss'],
  encapsulation: ViewEncapsulation.None
})

export class TopPanelLayoutComponent implements OnInit {
  public languageKeys: string[] = this.localizationService.languageKeys;
  public languages: any = this.localizationService.languages;
  public currentLanguage: string = this.localizationService.getCurrentLanguage();
  public navigationItems: CoopNavBarItem[] = [
    {label: 'TOP-PANEL.HOME', link: '/platform/home'},
    {label: 'TOP-PANEL.GAMES', link: '/platform/games'},
    {label: 'TOP-PANEL.CHATS', link: '/platform/chats'},
    {label: 'TOP-PANEL.MY-PROFILE', link: ''},
    {label: 'TOP-PANEL.FORUM', link: '#'},
    {label: 'TOP-PANEL.VACANCIES', link: '#'},
    {label: 'Statistics', link: '/platform/statistics'}
  ];

  constructor(public translate: TranslateService, public localizationService: LocalizationService,
              private sessionService: SessionService) {}

   public ngOnInit(): void {
    setTimeout(() => {
        this.navigationItems[3].link = '/platform/profile/' + this.sessionService.GetSessionUserNickname();
      }, 200);
   }

   public onLangChangeEvent(lang: string): void {
    this.localizationService.onLanguageChanged(lang);
  }
  public setDefaultLang(): void {
    this.translate.setDefaultLang(this.currentLanguage);
  }
  public onExitButtonClick(): void {
    localStorage.removeItem('JwtCooper');
    document.location.reload();
  }

}

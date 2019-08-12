import { Component, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CoopNavBarItem } from 'src.v2/app/models';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss'],
  encapsulation: ViewEncapsulation.None
})

export class TopPanelLayoutComponent {

  public navigationItems: CoopNavBarItem[] = [
    {label: 'TOP-PANEL.HOME', link: '#'},
    {label: 'TOP-PANEL.GAMES', link: '/platform/games'},
    {label: 'TOP-PANEL.CHATS', link: '#'},
    {label: 'TOP-PANEL.MY-PROFILE', link: '#'},
    {label: 'TOP-PANEL.FORUM', link: '#'},
    {label: 'TOP-PANEL.VACANCIES', link: '#'}
  ];

  constructor(public translate: TranslateService) {
   }

}

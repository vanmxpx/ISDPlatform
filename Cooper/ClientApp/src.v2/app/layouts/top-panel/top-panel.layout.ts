import { Component } from '@angular/core';
import { CoopNavBarItem } from 'src.v2/app/models/coop-navbar-item';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss']
})
export class TopPanelLayoutComponent {

  public navigationItems: CoopNavBarItem[] = [
    {label: 'Home', link: '#'},
    {label: 'Games', link: '/platform/games'},
    {label: 'Chats', link: '#'},
    {label: 'My profile', link: '#'},
    {label: 'Forum', link: '#'},
    {label: 'Vacancies', link: '#'}
  ];

}

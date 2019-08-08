import { Component, OnInit } from '@angular/core';
import { SessionService } from '@services';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss']
})
export class TopPanelLayoutComponent implements OnInit {
  navigationItems = [
    {label: 'Home', link: '/platform/home'},
    {label: 'Games', link: '/platform/games'},
    {label: 'Chats', link: '#'},
    { label: 'My profile', link: '#'},
    {label: 'Forum', link: '#'},
    {label: 'Vacancies', link: '#'}
  ];

  constructor(private sessionService: SessionService) { }

  ngOnInit() {
    this.navigationItems[3].link = '/platform/profile/' + this.sessionService.GetSessionUserNickname();
  }

}

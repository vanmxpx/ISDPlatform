import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss']
})
export class TopPanelLayoutComponent implements OnInit {
  navigationItems = [
    {label: 'Home', link: '#'},
    {label: 'Games', link: './games'}, 
    {label: 'Chats', link: '#'}, 
    {label: 'My profile', link: './profile'},  
    {label: 'Forum', link: '#'},
    {label: 'Vacancies', link: '#'} 
  ]

  constructor() { }

  ngOnInit() {
  }

}

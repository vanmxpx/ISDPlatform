import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'coop-top-panel-layout',
  templateUrl: './top-panel.layout.html',
  styleUrls: ['./top-panel.layout.scss']
})
export class TopPanelLayoutComponent implements OnInit {
  elements = [{label: 'Home', link: '#'}, {label: 'Vacans', link: '#'}, {label: 'Library', link: '#'}, {label: 'Teams', link: '#'} ]

  constructor() { }

  ngOnInit() {
  }

}

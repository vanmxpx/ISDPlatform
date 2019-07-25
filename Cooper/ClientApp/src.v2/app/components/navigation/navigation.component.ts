import { Component, OnInit, Input } from '@angular/core';
import {CoopNavBarItem} from "@models";

@Component({
  selector: 'coop-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  @Input() items: CoopNavBarItem[]

  constructor() { }

  ngOnInit() {
  }

}

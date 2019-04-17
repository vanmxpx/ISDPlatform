import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import {Observable} from "rxjs";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  constructor(
    private location: Location) { }

  ngOnInit() {
  }
  
  goBack(): void {
    this.location.back();
  }
}

import { Component, OnInit, Input } from '@angular/core';
import { Game } from '@models';

@Component({
  selector: 'coop-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.css']
})
export class GameCardComponent implements OnInit {

  @Input() game: Game;

  constructor() { }

  ngOnInit() {
  }

}

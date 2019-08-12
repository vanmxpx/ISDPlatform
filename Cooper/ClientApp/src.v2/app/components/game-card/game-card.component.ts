import { Component, Input } from '@angular/core';
import { Game } from '@models';

@Component({
  selector: 'coop-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {

  @Input() public game: Game;

}

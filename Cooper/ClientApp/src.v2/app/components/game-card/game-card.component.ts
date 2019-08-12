import { Component, Input } from '@angular/core';
import { Game } from '@models';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {

  @Input() public game: Game;

  constructor(public translate: TranslateService) { }

}

import { Component, Input } from '@angular/core';
import { Game } from '@models';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-game-list-item',
  templateUrl: './game-list-item.component.html',
  styleUrls: ['./game-list-item.component.scss']
})
export class GameListItemComponent {

  @Input() public game: Game;

}

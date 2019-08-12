import { Component, Input } from '@angular/core';
import {Game} from '@models';
@Component({
  selector: 'coop-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.css']
})
export class GamesListComponent {

  @Input() public games: Game[];

}

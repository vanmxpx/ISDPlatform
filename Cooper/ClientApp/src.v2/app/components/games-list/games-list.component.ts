import { Component, OnInit, Input } from '@angular/core';
import {Game} from '@models';
@Component({
  selector: 'coop-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.css']
})
export class GamesListComponent implements OnInit {

  constructor() { }

  @Input() games: Game[];

  ngOnInit() {
  }

}

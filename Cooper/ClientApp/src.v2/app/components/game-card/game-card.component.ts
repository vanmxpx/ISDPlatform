import { Component, OnInit, Input } from '@angular/core';
import { Game } from '@models';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent implements OnInit {

  @Input() game: Game;

  constructor(public translate: TranslateService) { }

  ngOnInit() {
  }

}

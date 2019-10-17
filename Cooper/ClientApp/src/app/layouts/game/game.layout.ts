import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from '@models';
import { GamesService } from '@services';
import {IComponentCanDeactivate} from '@guards';
import {Observable} from 'rxjs';

@Component({
  selector: 'coop-game-layout',
  templateUrl: './game.layout.html',
  styleUrls: ['./game.layout.css']
})
export class GameLayoutComponent implements OnInit, IComponentCanDeactivate {
  public game: Game;
  public url: string;

  constructor(private route: ActivatedRoute, private gameService: GamesService) { }

  public ngOnInit(): void {
    this.gameService.getGame(this.route.snapshot.params['link']).subscribe((data) => {
        this.game = data;
        this.url = 'proxy/' + this.game.link;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public canDeactivate(): boolean | Observable<boolean> {

    if (this.game) {
      return confirm('Do you really want to leave the page?');
    } else {
        return true;
    }
}
}

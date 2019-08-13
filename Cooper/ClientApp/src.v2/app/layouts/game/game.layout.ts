import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from '@models';
import { GamesService } from '@services';

@Component({
  selector: 'coop-game-layout',
  templateUrl: './game.layout.html',
  styleUrls: ['./game.layout.css']
})
export class GameLayoutComponent implements OnInit {
  game: Game;
  url: string;

  constructor(private route: ActivatedRoute, private gameService: GamesService) {
    this.game = {
      name: 'Loading...',
      logoUrl: '',
      link: '',
      description: 'Loading...',
      genre: 'Loading...'
    };
  }

  ngOnInit() {
    this.gameService.getGame(this.route.snapshot.params['link']).subscribe(data => {
        this.game = data;
        this.url = "/" + this.game.link;
      },
      err => {
        console.log(err);
      }
    );
  }
}

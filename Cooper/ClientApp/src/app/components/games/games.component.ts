import { Component, OnInit } from '@angular/core';
import { Game } from '../../models/game';
import { GameService } from '../../services/game.service';
import {Router } from '@angular/router';

@Component({
  selector: 'app-cooper-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: Game[];

  constructor(private gameService: GameService, private router: Router) { }

  ngOnInit() {
    this.getGames();
  }

  getGames(): void {
    this.gameService.getGames()
        .subscribe(games => this.games = games);
  }

  private openGame(game: Game): void {
    this.router.navigate([game.link]);
  }
}

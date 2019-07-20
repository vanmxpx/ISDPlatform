import { Component, OnInit } from '@angular/core';
import { Game } from 'src.v2/app/models/game-model';
import { GamesService } from 'src.v2/app/services/games.service';
import { listAnimation } from 'src.v2/app/helpers/animation.helper';

@Component({
  selector: 'coop-games-layout',
  templateUrl: './games.layout.html',
  styleUrls: ['./games.layout.css'],
  providers: [GamesService],
  animations: listAnimation
})

export class GamesLayoutComponent implements OnInit {

  games: Game[];
  isLoading: boolean = true;
  selectedTab: GameTabs = GameTabs.Cards;
  GameTabs = GameTabs;

  constructor(private gameService: GamesService) { }

  ngOnInit() {
    this.fetchData();  
  }

 async fetchData() {   
    const response = await this.gameService.getData();
    this.isLoading = false;
    this.games=response;
  }

  public onTabChange(val: string) {
    this.selectedTab = GameTabs[val];  
  }

}

enum GameTabs {
  Cards,
  List
}

import { Component, OnInit } from '@angular/core';
import { Game } from '@models';
import { GamesService } from '@services';
import { AnimationsHelper } from '@helpers';
import { GameListTabs } from '@enums';

@Component({
  selector: 'coop-games-layout',
  templateUrl: './games.layout.html',
  styleUrls: ['./games.layout.css'],
  providers: [GamesService],
  animations: AnimationsHelper.listAnimation
})

export class GamesLayoutComponent implements OnInit {

  games: Game[];
  isLoading: boolean = true;
  selectedTab: GameListTabs = GameListTabs.Cards;
  GameTabs = GameListTabs;

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
    this.selectedTab = GameListTabs[val];  
  }

}

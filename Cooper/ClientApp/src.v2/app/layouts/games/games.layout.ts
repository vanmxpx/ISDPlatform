import { Component, OnInit } from '@angular/core';
import { Game } from '@models';
import { GamesService } from '@services';
import { AnimationsHelper } from '@helpers';
import { GameListTabs } from '@enums';

@Component({
  selector: 'coop-games-layout',
  templateUrl: './games.layout.html',
  styleUrls: ['./games.layout.scss'],
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

  fetchData() {   
   this.gameService.getData()
    .subscribe((games: Game[]) => { 
      this.games = games;
      this.isLoading = false;
    },
    err => this.isLoading = false)
  }

  public onTabChange(val: string) {
    this.selectedTab = GameListTabs[val];  
  }

}

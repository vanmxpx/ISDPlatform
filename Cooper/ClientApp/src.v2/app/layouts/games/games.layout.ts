import { Component, OnInit } from '@angular/core';
import { Game } from '@models';
import { GamesService } from '@services';
import { AnimationsHelper } from '@helpers';
import { GameListTabs } from '@enums';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-games-layout',
  templateUrl: './games.layout.html',
  styleUrls: ['./games.layout.scss'],
  providers: [GamesService],
  animations: AnimationsHelper.listAnimation
})

export class GamesLayoutComponent implements OnInit {

  games: Game[];
  isLoading = true;
  selectedTab: GameListTabs = GameListTabs.Cards;
  GameTabs = GameListTabs;

  constructor(private gameService: GamesService, public translate: TranslateService) { }

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
   this.gameService.getGamesData()
    .subscribe((games: Game[]) => {
      this.games = games;
      this.isLoading = false;
    },
    err => this.isLoading = false);
  }

  public onTabChange(val: string) {
    this.selectedTab = GameListTabs[val];
  }

}

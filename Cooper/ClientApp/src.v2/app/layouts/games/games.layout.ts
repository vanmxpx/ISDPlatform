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

  public games: Game[];
  public isLoading: boolean = true;
  public selectedTab: GameListTabs = GameListTabs.Cards;
  public GameTabs: typeof GameListTabs = GameListTabs;

  constructor(private gameService: GamesService, public translate: TranslateService) { }
  public ngOnInit(): void {
    this.fetchData();
  }
  public fetchData(): void {
   this.gameService.getData()
    .subscribe((games: Game[]) => {
      this.games = games;
      this.isLoading = false;
    },
    (_) => this.isLoading = false);
  }

  public onTabChange(val: string): void {
    this.selectedTab = GameListTabs[val];
  }

}

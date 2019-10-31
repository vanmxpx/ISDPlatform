import { Component } from '@angular/core';
import {SessionService, GameStatisticsService } from '@services';
import { GameStatistics } from '@models';

@Component({
  selector: 'coop-home',
  templateUrl: './home.layout.html',
  styleUrls: ['./home.layout.scss']
})
export class HomeLayoutComponent {
  public statistics: GameStatistics;

  constructor(private sessionService: SessionService, private gameStatistics: GameStatisticsService) {
    this.refresh();
  }

  private refresh(): void {
    this.gameStatistics.getStatistics(this.sessionService.GetSessionUserId(), 21).subscribe((statistics) => {
      this.statistics = statistics;
      this.refresh();
    });
  }
}

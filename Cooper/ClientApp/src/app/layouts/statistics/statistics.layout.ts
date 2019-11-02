import { Component } from '@angular/core';
import { SessionService, GameStatisticsService } from '@services';
import { GameStatistics } from '@models';

@Component({
  selector: 'coop-statistics',
  templateUrl: './statistics.layout.html',
  styleUrls: ['./statistics.layout.css']
})
export class StatisticsLayoutComponent {
  public statistics: GameStatistics = null;

  constructor(private sessionService: SessionService, private gameStatistics: GameStatisticsService) {
      this.refresh();
  }

  private refresh(): void {
    this.gameStatistics.getStatistics(this.sessionService.GetSessionUserId(), 21).subscribe((statistics) => {
      this.statistics = statistics;
      console.log(statistics);
    });
  }
}

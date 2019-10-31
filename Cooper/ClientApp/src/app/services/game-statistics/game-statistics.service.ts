import { Injectable } from '@angular/core';
import { GameStatistics } from '@models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const getGamesUrl: string = '/api/game/statistics/get';

@Injectable({
  providedIn: 'root'
})
export class GameStatisticsService {

  constructor(private httpClient: HttpClient) { }

  public getStatistics(userId: number, gameId: number): Observable<GameStatistics> {
    const url = getGamesUrl + '?userId=' + userId + '&gameId=' + gameId;
    return this.httpClient.get<GameStatistics>(url);
  }
}

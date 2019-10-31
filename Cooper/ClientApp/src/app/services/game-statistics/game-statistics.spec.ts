import { TestBed } from '@angular/core/testing';

import { GameStatisticsService } from './game-statistics.service';

describe('GamesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GameStatisticsService = TestBed.get(GameStatisticsService);
    expect(service).toBeTruthy();
  });
});

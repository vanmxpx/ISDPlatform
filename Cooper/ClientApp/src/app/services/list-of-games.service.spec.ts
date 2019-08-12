import { TestBed } from '@angular/core/testing';

import { ListOfGamesService } from './list-of-games.service';

describe('ListOfGamesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ListOfGamesService = TestBed.get(ListOfGamesService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { UsersInteractionService } from './users-interaction.service';

describe('UsersInteractionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UsersInteractionService = TestBed.get(UsersInteractionService);
    expect(service).toBeTruthy();
  });
});

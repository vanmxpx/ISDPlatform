import { TestBed } from '@angular/core/testing';

import { UsersConnectionService } from './users-interaction.service';

describe('UsersConnectionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UsersConnectionService = TestBed.get(UsersConnectionService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { UsersSocialConnectionService } from './users-social-connection.service';

describe('UsersSocialConnectionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UsersSocialConnectionService = TestBed.get(UsersSocialConnectionService);
    expect(service).toBeTruthy();
  });
});

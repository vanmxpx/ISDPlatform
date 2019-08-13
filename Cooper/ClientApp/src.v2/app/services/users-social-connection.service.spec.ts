import { TestBed } from '@angular/core/testing';

import { UsersSocialConnectionsService } from './users-social-connections.service';

describe('UsersSocialConnectionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UsersSocialConnectionsService = TestBed.get(UsersSocialConnectionsService);
    expect(service).toBeTruthy();
  });
});

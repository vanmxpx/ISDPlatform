import { TestBed } from '@angular/core/testing';

import { UsersFriendsService } from './users-friends.service';

describe('UsersFriendsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UsersFriendsService = TestBed.get(UsersFriendsService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { ResetPasswordService } from './reset-password.service';

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ResetPasswordService = TestBed.get(ResetPasswordService);
    expect(service).toBeTruthy();
  });
});

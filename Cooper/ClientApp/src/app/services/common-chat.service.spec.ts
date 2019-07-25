import { TestBed } from '@angular/core/testing';

import { CommonChatService } from './common-chat.service';

describe('CommonChatService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommonChatService = TestBed.get(CommonChatService);
    expect(service).toBeTruthy();
  });
});

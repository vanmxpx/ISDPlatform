import { TestBed } from '@angular/core/testing';

import { LanguageStorageService } from './language-storage.service';

describe('LanguageStorageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LanguageStorageService = TestBed.get(LanguageStorageService);
    expect(service).toBeTruthy();
  });
});

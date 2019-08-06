import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageStorageService {
  getLanguage(): string {
    if (localStorage) {
        return localStorage.getItem('language') || 'en';
    } else {
        return 'en';
    }
}

setLanguage(language: string) {
    if (localStorage) {
        localStorage.setItem('language', language);
    }
}

  constructor() { }
}

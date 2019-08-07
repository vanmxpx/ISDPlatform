import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageStorageService {
  defoultLanguage = 'en';
  getLanguage(): string {
    if (localStorage) {
        return localStorage.getItem('language') || this.defoultLanguage;
    } else {
        return this.defoultLanguage;
    }
}

setLanguage(language: string) {
    if (localStorage) {
        localStorage.setItem('language', language);
    }
}

  constructor() {
  }
}

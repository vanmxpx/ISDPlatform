import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor() { }

    GetSessionUserNickname(): string {

    const token = localStorage.getItem('JwtCooper');
    const payload = atob(token.split('.')[1]);

    const nickname = JSON.parse(payload).username;

    return nickname;
  }
}

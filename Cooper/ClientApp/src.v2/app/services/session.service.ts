import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from '@models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private sessionProfile: User;

  constructor(private httpClient: HttpClient) {
      this.fetchSessionProfileData();
   }

  async fetchSessionProfileData() {
    this.sessionProfile = await this.getSessionUserData();
  }

  async getSessionUserData(): Promise<User> {
    const response = await this.httpClient.get<User>('/users/token').toPromise();
    return response;
  }

  GetSessionUserNickname(): string {

    return this.sessionProfile.nickname;
  }

  public IsSessionProfile(profile: User): boolean {
    return this.sessionProfile.id === profile.id &&
    this.sessionProfile.nickname === profile.nickname &&
    this.sessionProfile.name === profile.name &&
    this.sessionProfile.email === profile.email &&
    this.sessionProfile.description === profile.description;
  }
}

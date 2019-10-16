import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from '@models';
import {UsersSocialConnectionsService} from '@services';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private sessionProfile: User;
  private subscriptionsList: User[];

  constructor(private httpClient: HttpClient,
              private userSocialService: UsersSocialConnectionsService) {
      this.fetchSessionProfileData();
   }

  public async fetchSessionProfileData(): Promise<any> {
    this.sessionProfile = await this.getSessionUserData();
    this.subscriptionsList = await this.userSocialService.getSubscriptions(this.sessionProfile.id);
  }

  public async getSessionUserData(): Promise<User> {
    const response = await this.httpClient.get<User>('/users/token').toPromise();
    return response;
  }

  public GetSessionUserNickname(): string {

    return this.sessionProfile.nickname;
  }

  public GetSessionUserId(): number {

    return this.sessionProfile.id;
  }

  public isSubscription(userId: number): boolean {
    for (const user of this.subscriptionsList) {
      if (user.id === userId) {
        return true;
      }
    }

    return false;
  }

  public IsSessionProfile(profile: User): boolean {
    return this.sessionProfile.id === profile.id &&
    this.sessionProfile.nickname === profile.nickname &&
    this.sessionProfile.name === profile.name &&
    this.sessionProfile.email === profile.email &&
    this.sessionProfile.description === profile.description;
  }
}

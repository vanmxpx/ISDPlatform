import { Injectable } from '@angular/core';
import { ProfileList } from '../models/my-page-models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersFriendsService {
  
  constructor(private httpClient: HttpClient) {  }

    async getFriends(): Promise <ProfileList[]>{
      const response = await this.httpClient.get<Array<ProfileList>>("/users").toPromise();
      return response;
    } 

    async getFollowers(): Promise <ProfileList[]>{
      const response = await this.httpClient.get<Array<ProfileList>>("/users").toPromise();
      return response;
    } 

    async getFollowings(): Promise <ProfileList[]>{
      const response = await this.httpClient.get<Array<ProfileList>>("/users").toPromise();
      return response;
    } 

}

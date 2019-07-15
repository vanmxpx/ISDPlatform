import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Profile } from '../models/my-page-models';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  async getUsersProfile(nickname: string): Promise<Profile>{
    const response = await this.httpClient.get<Profile>("/users/nickname/" + nickname).toPromise();
    return response;
  }

  constructor(private httpClient: HttpClient) { }
  
}

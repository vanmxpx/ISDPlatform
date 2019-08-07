import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '@models';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  async getUserByNickname(nickname: string): Promise<User> {
    const response = await this.httpClient.get<User>('/users/nickname/' + nickname).toPromise();
    return response;
  }


}

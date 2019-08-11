import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '@models';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public async getUserByNickname(nickname: string): Promise<User> {
    const response = await this.httpClient.get<User>('/users/nickname/' + nickname).toPromise();
    return response;
  }

  public updateUserInfo(user: User): Subscription {
    return this.httpClient.post('/users', user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('User with id {0} was succesfully updated.', user.id);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }
}

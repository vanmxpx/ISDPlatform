import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  postData(user: User) {
    return this.httpClient.post('/users', user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe((res: User) => {
      console.log('User with id {0} was succesfully updated.', user.id);
    },
      err => {
        console.log('Error: {0}', err);
      });
  }
}

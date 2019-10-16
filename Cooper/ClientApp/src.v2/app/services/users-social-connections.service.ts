import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '@models';
import { timeout, catchError } from 'rxjs/operators';

const userSocialConnectionUrl = '/socialConnections';

@Injectable({
  providedIn: 'root'
})
export class UsersSocialConnectionsService {
  constructor(private http: HttpClient) { }

  public async getFriends(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(userSocialConnectionUrl + '/friends/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking friends for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }

  public async getSubscribers(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(userSocialConnectionUrl + '/subscribers/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking subscribers for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }

  public async getSubscriptions(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(userSocialConnectionUrl + '/subscriptions/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking subscriptions for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;

  }

  public async getBlacklist(): Promise<User[]> {

    const response = await this.http.get<User[]>(userSocialConnectionUrl + '/blacklist').pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking blacklist for currect session user');

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }

  public subscribe(userId: number): void {
    this.http.post(userSocialConnectionUrl + `/subscribe/${userId}`, {}).subscribe(() => {
      console.log(`Subscribtion on user with id = ${userId}`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }

  public unsubscribe(userId: number): void {
    this.http.delete(userSocialConnectionUrl + `/${userId}`).subscribe(() => {
      console.log(`Unsubscribed from user with id = ${userId}`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }

  public ban(userId: number): void {
    this.http.post(userSocialConnectionUrl + `/ban/${userId}`, {}).subscribe(() => {
      console.log(`Banned user with id = ${userId}`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }

  public unban(userId: number): void {
    this.http.post(userSocialConnectionUrl + `/unban/${userId}`, {}).subscribe(() => {
      console.log(`Unbanned user with id = ${userId}`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }

}

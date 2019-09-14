import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '@models';
import { timeout, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersInteractionService {

  private readonly userInteractionUrl: string = '/interaction';

  constructor(private http: HttpClient) { }

  public async getFriends(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(this.userInteractionUrl + '/friends/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking friends for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }

  public async getSubscribers(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(this.userInteractionUrl + '/subscribers/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking subscribers for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }

  public async getSubscriptions(userId: number): Promise<User[]> {

    const response = await this.http.get<User[]>(this.userInteractionUrl + '/subscriptions/' + userId.toString()).pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking subscriptions for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;

  }

  public async getBlacklist(): Promise<User[]> {

    const response = await this.http.get<User[]>(this.userInteractionUrl + '/blacklist').pipe(timeout(3000),
    catchError(() => {
      console.log('Too long respond for taking blacklist for currect session user');

      // do smth then
      return of(null);
    })
    ).toPromise();

    return response;
  }
}

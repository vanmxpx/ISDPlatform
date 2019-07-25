import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../../models/user';
import { timeout, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersInteractionService {

  private readonly userInteractionUrl = '/interaction';

  constructor(private http: HttpClient) { }

  public getFriends(userId: number): Observable<User[]> {

    return this.http.get<User[]>(this.userInteractionUrl + '/friends/' + userId.toString()).pipe(timeout(3000),
    catchError(e => {
      console.log('Too long respond for taking friends for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    );

  }

  public getSubscribers(userId: number): Observable<User[]> {

    return this.http.get<User[]>(this.userInteractionUrl + '/subscribers/' + userId.toString()).pipe(timeout(3000),
    catchError(e => {
      console.log('Too long respond for taking subscribers for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    );

  }

  public getSubscriptions(userId: number): Observable<User[]> {

    return this.http.get<User[]>(this.userInteractionUrl + '/subscriptions/' + userId.toString()).pipe(timeout(3000),
    catchError(e => {
      console.log('Too long respond for taking subscriptions for user whose Id =' + userId.toString());

      // do smth then
      return of(null);
    })
    );

  }

  public getBlacklist(): Observable<User[]> {

    return this.http.get<User[]>(this.userInteractionUrl + '/blacklist').pipe(timeout(3000),
    catchError(e => {
      console.log('Too long respond for taking blacklist for currect session user');

      // do smth then
      return of(null);
    })
    );

  }
}

import { Injectable } from '@angular/core';
import { of, Observable} from 'rxjs';
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

  public subscribeOnUser(userId: number): Observable<boolean> {
    return this.http.post<boolean>(userSocialConnectionUrl + `/subscribe/${userId}`, {})
    .pipe(
      catchError(this.handleError<boolean>(`subscription on user with id = ${userId}`, false)));
  }

  public unsubscribeFromUser(userId: number): Observable<boolean> {
    return this.http.delete<boolean>(userSocialConnectionUrl + `/${userId}`)
    .pipe(
      catchError(this.handleError<boolean>(`unsubscription from user with id = ${userId}`, false)));
  }

  public ban(userId: number): Observable<boolean> {
    return this.http.post<boolean>(userSocialConnectionUrl + `/ban/${userId}`, {})
    .pipe(catchError(this.handleError<boolean>(`Banned user with id = ${userId}`, false)));
  }

  public unban(userId: number): Observable<boolean> {
    return this.http.post<boolean>(userSocialConnectionUrl + `/unban/${userId}`, {})
    .pipe(catchError(this.handleError<boolean>(`Unbanned user with id = ${userId}`, false)));
  }

  private handleError<T>(operation: string = 'operation', result?: T): any {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}

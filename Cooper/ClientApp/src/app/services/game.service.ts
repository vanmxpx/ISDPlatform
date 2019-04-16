import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Game } from '../models/game';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
//import { MessageService } from './message.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class GameService {

  private gamesUrl = 'api/games';  // URL to web api

  constructor(  
    private http: HttpClient,
    //private messageService: MessageService) { }

  /** GET gamees from the server */
  getGames (): Observable<Game[]> {
    return this.http.get<Game[]>(this.gamesUrl)
      .pipe(
        tap(_ => this.log('fetched games')),
        catchError(this.handleError<Game[]>('getGames', []))
      );
  }

  /** GET game by id. Will 404 if id not found */
  getGame(id: number): Observable<Game> {
    const url = `${this.gamesUrl}/${id}`;
    return this.http.get<Game>(url).pipe(
      tap(_ => this.log(`fetched game id=${id}`)),
      catchError(this.handleError<Game>(`getGame id=${id}`))
    );
  }

  /** PUT: update the game on the server */
  updateGame (game: Game): Observable<any> {
    return this.http.put(this.gamesUrl, game, httpOptions).pipe(
      tap(_ => this.log(`updated game id=${game.id}`)),
      catchError(this.handleError<any>('updateGame'))
    );
  }

  /** POST: add a new game to the server */
  addGame (game: Game): Observable<Game> {
    return this.http.post<Game>(this.gamesUrl, game, httpOptions).pipe(
      tap((newGame: Game) => this.log(`added game w/ id=${newGame.id}`)),
      catchError(this.handleError<Game>('addGame'))
    );
  }

  /** DELETE: delete the game from the server */
  deleteGame (game: Game | number): Observable<Game> {
    const id = typeof game === 'number' ? game : game.id;
    const url = `${this.gamesUrl}/${id}`;

    return this.http.delete<Game>(url, httpOptions).pipe(
      tap(_ => this.log(`deleted game id=${id}`)),
      catchError(this.handleError<Game>('deleteGame'))
    );
  }

  /* GET gamees whose name contains search term */
  searchGames(term: string): Observable<Game[]> {
    if (!term.trim()) {
      // if not search term, return empty game array.
      return of([]);
    }
    return this.http.get<Game[]>(`${this.gamesUrl}/?name=${term}`).pipe(
      tap(_ => this.log(`found games matching "${term}"`)),
      catchError(this.handleError<Game[]>('searchGames', []))
    );
  }

  /** Log a GameService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`GameService: ${message}`);
  }

    /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
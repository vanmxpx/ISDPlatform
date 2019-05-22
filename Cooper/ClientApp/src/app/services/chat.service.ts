import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Chat } from '../models/chat';
import { catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private chatsUrl = '/chats';  // URL to web api

  constructor(  
    private http: HttpClient
    ) 
    { }

  /** GET chats from the server */
  getChats(): Observable<Chat[]> {
    return this.http.get<Chat[]>(this.chatsUrl)
      .pipe(
        catchError(this.handleError<Chat[]>('getChats', []))
      );
  }

  /** GET chat by id. Will 404 if id not found */
  getChat(id: number): Observable<Chat> {
    const url = `${this.chatsUrl}/${id}`;
    return this.http.get<Chat>(url).pipe(
      catchError(this.handleError<Chat>(`getChat id=${id}`))
    );
  }

  /** PUT: update the chat on the server */
  updateChat (chat: Chat): Observable<Chat> {
    return this.http.put(this.chatsUrl, chat, httpOptions).pipe(
      catchError(this.handleError<any>('updateChat'))
    );
  }

  /** POST: add a new chat to the server */
  addChat (chat: Chat): Observable<Chat> {
    return this.http.post<Chat>(this.chatsUrl, chat, httpOptions).pipe(
      catchError(this.handleError<Chat>('addChat'))
    );
  }

  /** DELETE: delete the chat from the server */
  deleteChat (chat: Chat | number): Observable<Chat> {
    const id = typeof chat === 'number' ? chat : chat.id;
    const url = `${this.chatsUrl}/${id}`;

    return this.http.delete<Chat>(url, httpOptions).pipe(
      catchError(this.handleError<Chat>('deleteChat'))
    );
  }

  /* GET chates whose name contains search term */
  searchChats(term: string): Observable<Chat[]> {
    if (!term.trim()) {
      // if not search term, return empty chat array.
      return of([]);
    }
    return this.http.get<Chat[]>(`${this.chatsUrl}/?name=${term}`).pipe(
      catchError(this.handleError<Chat[]>('searchChats', []))
    );
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
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

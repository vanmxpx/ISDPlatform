import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Message } from '../models/message';
import { catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class MessageService {

  private messagesUrl = '/chat/messages';  // URL to web api

  constructor(  
    private http: HttpClient
    ) 
    { }

  /** GET messages from the server */
  getMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.messagesUrl)
      .pipe(
        catchError(this.handleError<Message[]>('getMessages', []))
      );
  }

  /** GET message by id. Will 404 if id not found */
  getMessage(id: number): Observable<Message> {
    const url = `${this.messagesUrl}/${id}`;
    return this.http.get<Message>(url).pipe(
      catchError(this.handleError<Message>(`getMessage id=${id}`))
    );
  }

  /** PUT: update the message on the server */
  updateMessage (message: Message): Observable<Message> {
    return this.http.put(this.messagesUrl, message, httpOptions).pipe(
      catchError(this.handleError<any>('updateMessage'))
    );
  }

  /** POST: add a new message to the server */
  addMessage (message: Message): Observable<Message> {
    return this.http.post<Message>(this.messagesUrl, message, httpOptions).pipe(
      catchError(this.handleError<Message>('addMessage'))
    );
  }

  /** DELETE: delete the message from the server */
  deleteMessage (message: Message | number): Observable<Message> {
    const id = typeof message === 'number' ? message : message.id;
    const url = `${this.messagesUrl}/${id}`;

    return this.http.delete<Message>(url, httpOptions).pipe(
      catchError(this.handleError<Message>('deleteMessage'))
    );
  }

  /* GET messages whose name contains search term */
  searchMessages(term: string): Observable<Message[]> {
    if (!term.trim()) {
      // if not search term, return empty message array.
      return of([]);
    }
    return this.http.get<Message[]>(`${this.messagesUrl}/?name=${term}`).pipe(
      catchError(this.handleError<Message[]>('searchMessages', []))
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

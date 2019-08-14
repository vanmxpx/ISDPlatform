import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  constructor(private httpClient: HttpClient) { }

  public send(email: string): Subscription {
    const body = {
      Email: email
    };
    return this.httpClient.post('/reset/send', body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('Password reset email was sent successfully to \'{0}\'.', email);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }
}

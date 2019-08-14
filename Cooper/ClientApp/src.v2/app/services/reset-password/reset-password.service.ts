import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  constructor(private router: Router, private httpClient: HttpClient) { }

  public send(email: string): Subscription {

    if (!email) {
      this.redirectToResetPage('Email is required');
      return null;
    }

    const body = {
      Email: email
    };

    return this.httpClient.post('/reset/send', body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('Password reset email was sent successfully to \'{0}\'.', email);
      this.redirectToResetPage();
    },
      (err) => {
        console.log('Error: {0}', err);
        this.redirectToResetPage('Email does not exist');
      });
  }

  public redirectToResetPage(failedMessage?: string): void {
    if (failedMessage) {
      this.router.navigate(['/reset', {failedReset: true, failed: failedMessage}]);
    } else {
      this.router.navigate(['/reset']);
    }
  }
}

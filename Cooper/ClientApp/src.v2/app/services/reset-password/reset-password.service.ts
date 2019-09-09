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

    return this.httpClient.post('/auth/reset/send', body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('Password reset email was sent successfully to ', email);
      this.redirectToResetPage();
    },
      (err) => {
        console.log('Password reset email was not sent to ', email);
        console.log('Error: ', err);
        this.redirectToResetPage('Email does not exist');
      });
  }

  public redirectToResetPage(failedMessage?: string): void {
    if (failedMessage) {
      this.router.navigate(['/reset', { failedReset: true, failed: failedMessage }]);
    } else {
      this.router.navigate(['/reset']);
    }
  }

  public resetPassword(token: string, newPassword: string): Subscription {
    if (!token && !newPassword) {
      return null;
    }

    const body = {
      Token: token,
      NewPassword: newPassword
    };

    return this.httpClient.post('/auth/reset', body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('Password was reset successfully.');
    },
      (err) => {
        console.log('Password was not reset.');
        console.log('Error: ', err);
      });
  }
}

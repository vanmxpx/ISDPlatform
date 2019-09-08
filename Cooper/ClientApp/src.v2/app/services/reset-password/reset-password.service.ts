import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {

  constructor(private router: Router, private httpClient: HttpClient) { }

  public send(email: string): Subscription {

    if (!email) {
      this.redirectToResetPage('Email is required', true);
      return null;
    }

    const body = {
      Email: email
    };

    return this.httpClient.post('/auth/reset/send', body).subscribe(() => {
      console.log('Password reset email was sent successfully to ', email);
      this.redirectToResetPage('The password reset email has been sent to your email!', false);
    },
      (err) => {
        console.log('Password reset email was not sent to ', email);
        console.log('Error: ', err);
        this.redirectToResetPage('Email does not exist', true);
      });
  }

  public resetPassword(token: string, newPassword: string): Observable<any> {
    if (!token && !newPassword) {
      return null;
    }

    const body = {
      Token: token,
      Password: newPassword
    };

    return this.httpClient.post('/auth/reset', body);
  }

  public redirectToResetPage(message?: string, error?: boolean): void {
    if (message && error) {
      this.router.navigate(['/reset', {failed: true, msg: message}]);
    } else if (message && !error) {
      this.router.navigate(['/reset', {notified: true, msg: message}]);
    } else {
      this.router.navigate(['/reset']);
    }
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'ng-dynami-social-login';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  readonly registrationUrl;
  readonly authUrl;
  readonly socialProvider;

  constructor(private router: Router, private http: HttpClient, private socialAuthService: AuthService) {
    this.registrationUrl = '/registration';
    this.authUrl = '/auth/login';
    this.socialProvider = {
      'google': GoogleLoginProvider.PROVIDER_ID,
      'facebook': FacebookLoginProvider.PROVIDER_ID
    };
   }

  public checkAuthentification(): void {
    const token: string = localStorage.getItem('JwtCooper');
    if (token) {
      this.router.navigate(['/platform/profile']);
    }
  }

  public signIn(credentials: string): void {
    this.http.post(this.authUrl, credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.loginOK((response as any).token);
        }, err => {
      this.BadLogin();
    });
  }

  public socialSignIn(socialPlatform: string) {
    this.socialAuthService.signIn(this.socialProvider[socialPlatform]).then(
      (userData) => {
        if (socialPlatform === 'google') {
          userData.id = userData.email;
          userData.token = userData.idToken;
        }
        this.transferSocialDataToServer(userData);
      }
    );
  }

  private transferSocialDataToServer(userData: any) {
    let body = this.createBody(userData, true);

    this.http.post(this.authUrl, body).subscribe(
      response => {
        this.loginOK((response as any).token);
      },
      err => {
        if (err.error === 'Auth') {
          // Register
          body = this.createBody(userData, false);
          this.http.post(this.registrationUrl, body).subscribe(
            response => {
              // Try to login 1 time
              body = this.createBody(userData, true);
              this.http.post(this.authUrl, body).subscribe(
                response => {
                  this.loginOK((response as any).token);
                },
                err => {
                  this.BadLogin();
                }
              );
            }
          );
        } else {
          this.BadLogin();
        }
      }
    );
  }

  loginOK(token: string): void {
    localStorage.setItem('JwtCooper', token);
    this.router.navigate(['/platform/profile']);
  }

  public BadLogin(): void {
    this.router.navigate(['/login', {failedLogin: true}]);
  }

  createBody(userData, isAuth: boolean) {
    let body;
    if (isAuth) {
      body = {
        Username: userData.email,
        ID: userData.id,
        Provider: userData.provider,
        Password: userData.token
      };
    } else {
      body = {
        Name: userData.name,
        Email: userData.email,
        Nickname: userData.id,
        Password: userData.token,
        Provider: userData.provider,
        PhotoURL: userData.image
      };
    }

    return body;
  }
}

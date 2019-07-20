import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'ng-dynami-social-login';
import { SocialNetwork} from '@enums';

class LoginAttemptResponse {
  token: string;
}

const registrationUrl = '/registration';
const authUrl = '/auth/login';
const socialProvider = {
      'google': GoogleLoginProvider.PROVIDER_ID,
      'facebook': FacebookLoginProvider.PROVIDER_ID
    };

@Injectable({
  providedIn: 'root'
})


export class AuthentificationService {

  constructor(private router: Router, private http: HttpClient, private socialAuthService: AuthService) {
   }

  public isAuthentificated(): boolean {
    const token: string = localStorage.getItem('JwtCooper');

    if (token) {
      return true;
    }

    return false;
  }

  public signIn(credentials: string): void {
    this.http.post(authUrl, credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.onPassedAuth((response as LoginAttemptResponse).token);
        }, err => {
      this.redirectToLoginPage();
    });
  }

  public socialSignIn(socialPlatform: SocialNetwork) {
    this.socialAuthService.signIn(socialProvider[socialPlatform]).then(
      (userData) => {
        if (socialPlatform === SocialNetwork.Google) {
          userData.id = userData.email;
          userData.token = userData.idToken;
        }
        this.transferSocialDataToServer(userData);
      }
    );
  }

  private transferSocialDataToServer(userData: any) {
    let body = this.createBody(userData, true);

    this.http.post(authUrl, body).subscribe(
      response => {
        this.onPassedAuth((response as LoginAttemptResponse).token);
      },
      err => {
        if (err.error === 'Auth') {
          // Register
          body = this.createBody(userData, false);
          this.http.post(registrationUrl, body).subscribe(
            response => {
              // Try to login 1 time
              body = this.createBody(userData, true);
              this.http.post(authUrl, body).subscribe(
                response => {
                  this.onPassedAuth((response as LoginAttemptResponse).token);
                },
                err => {
                  this.redirectToLoginPage();
                }
              );
            }
          );
        } else {
          this.redirectToLoginPage();
        }
      }
    );
  }

  onPassedAuth(token: string): void {
    localStorage.setItem('JwtCooper', token);
    this.router.navigate(['/platform/home']);
  }

  public redirectToLoginPage(): void {
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

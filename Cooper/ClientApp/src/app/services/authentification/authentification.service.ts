import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'ng-dynami-social-login';
import { SocialNetwork} from '@enums';

class LoginAttemptResponse {
  public token: string;
}

const registrationUrl = '/registration';
const authUrl = '/auth/login';
const socialProvider = {
      google: GoogleLoginProvider.PROVIDER_ID,
      facebook: FacebookLoginProvider.PROVIDER_ID
    };

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {
  public registerEvent: EventEmitter<boolean> = new EventEmitter();

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
    }).subscribe((response) => {
      this.onPassedAuth((response as LoginAttemptResponse).token);
        }, () => {
      this.redirectToLoginPage();
    });
  }

  public socialSignIn(socialPlatform: SocialNetwork): void {
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

  private transferSocialDataToServer(userData: any): void {
    const body = this.createBody(userData, null, true);

    this.http.post(authUrl, body).subscribe(
      (response) => {
        this.onPassedAuth((response as LoginAttemptResponse).token);
      },
      (err) => {
        if (err.error === 'Auth') {
          // Register
          this.registerEvent.emit(userData);
        } else {
          this.redirectToLoginPage();
        }
      }
    );
  }

  public socialRegister(userData: any, nickname: string): void {
    let body = this.createBody(userData, nickname, false);
    this.http.post(registrationUrl, body).subscribe(
      () => {
      // Try to login 1 time
      body = this.createBody(userData, nickname, true);
      this.http.post(authUrl, body).subscribe(
        (response) => {
          this.onPassedAuth((response as LoginAttemptResponse).token);
        },
        () => {
          this.redirectToLoginPage();
        }
      );
    }
    );
  }

  public onPassedAuth(token: string): void {
    localStorage.setItem('JwtCooper', token);
    this.router.navigate(['/platform/home']);
  }

  public redirectToLoginPage(): void {
    this.router.navigate(['/login', {failedLogin: true}]);
  }

  public createBody(userData: any, nickname: string, isAuth: boolean): any {
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
        Id: userData.id,
        Nickname: nickname,
        Password: userData.token,
        Provider: userData.provider,
        PhotoURL: userData.image
      };
    }

    return body;
  }
}

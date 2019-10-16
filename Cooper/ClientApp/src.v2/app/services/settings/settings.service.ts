import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'ng-dynami-social-login';
import { SocialNetwork } from '@enums';

const settingsUrl = '/settings/';
const socialProvider = {
      google: GoogleLoginProvider.PROVIDER_ID,
      facebook: FacebookLoginProvider.PROVIDER_ID
    };

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  public updateAccountEvent: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient, private socialAuthService: AuthService) {}

  public socialConnect(socialPlatform: SocialNetwork): void {
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

  public changeEmail(newEmail: string): void {
    this.http.post(settingsUrl + 'email', { newEmail }).subscribe(
        (response) => {
            console.log(response);
            this.updateAccountEvent.emit();
        },
        () => {
            console.log('Error: Can\'t change email!');
        }
      );
  }

  public deleteAccount(): void {
    this.http.post(settingsUrl + 'delete', '').subscribe(
        (response) => {
            console.log(response);
            this.updateAccountEvent.emit();
        },
        () => {
            console.log('Error: Can\'t delete account!');
        }
      );
  }

  private transferSocialDataToServer(userData: any): void {
    const body = this.createBody(userData, null, true);

    this.http.post(settingsUrl + 'social', body).subscribe(
      (response) => {
          console.log(response);
          this.updateAccountEvent.emit();
      },
      () => {
          console.log('Error: Can\'t connect social!');
      }
    );
  }

  private createBody(userData: any, nickname: string, isAuth: boolean): any {
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

import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'ng-dynami-social-login';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private router: Router, private http: HttpClient, private socialAuthService: AuthService) { }
  public user: any;
  readonly registrationUrl = '/registration';
  readonly authUrl = '/auth/login';

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],

    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      Name: this.formModel.value.UserName,
      Nickname: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.registrationUrl, body);
  }

  public socialSignIn(socialPlatform : string) {
    let socialPlatformProvider;
    if (socialPlatform == "facebook"){
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    } else if (socialPlatform == "google"){
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }

    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {
        console.log(userData);
        this.socialsignin(userData);
      }
    );
  }

  socialsignin(userData) {
    var body = this.bodyCreator(userData, true);

    this.http.post(this.authUrl, body).subscribe(
      response => {
        this.loginOK((<any>response).token, this.router);
      },
      err => {
        if (err.error == "Auth") {
          //Register
          body = this.bodyCreator(userData, false);
          this.http.post(this.registrationUrl, body).subscribe(
            response => {
              //Try to login 1 time
              body = this.bodyCreator(userData, true);
              console.log(userData);
              this.http.post(this.authUrl, body).subscribe(
                response => {
                  this.loginOK((<any>response).token, this.router);
                },
                err => {
                  this.BadLogin(this.router);
                }
              );
            }
          );
        }
        else 
        {
          this.BadLogin(this.router);
        }
      }
    );  
  }

  public loginOK(token, router) {
    localStorage.setItem("JwtCooper", token);
    router.navigate(['/myPage', "my"]);
  }

  public BadLogin(router) {
    router.navigate(["/myPage", "my"]);
  }

  bodyCreator(userData, login) {
    let result;
    if (login == true)
    {
      if (userData.provider == "facebook") {
        result = {
          Username: userData.email,
          ID: userData.id,
          Provider: userData.provider,
          Password: userData.token
        };
      }
      else if (userData.provider == "google")
      {
        result = {
          Username: userData.email,
          ID: userData.id,
          Provider: userData.provider,
          Password: userData.idToken
        };
      }
    }
    else 
    {
      if (userData.provider == "facebook") {
        result = {
          Name: userData.name,
          Email: userData.email,
          Nickname: userData.id,
          Password: userData.token,
          Provider: userData.provider,
          PhotoURL: userData.image
        };
      }
      else if (userData.provider == "google")
      {
        result = {
          Name: userData.name,
          Email: userData.email,
          Nickname: userData.id,
          Password: userData.idToken,
          Provider: userData.provider,
          PhotoURL: userData.image
        };
      }
    }

    return result;
  }
}

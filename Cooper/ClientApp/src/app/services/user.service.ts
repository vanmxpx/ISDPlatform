import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
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
  readonly getProvider = {
    "google": GoogleLoginProvider.PROVIDER_ID,
    "facebook": FacebookLoginProvider.PROVIDER_ID
  }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],

    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  public CheckAuthentification(): void {
    const Token: string = localStorage.getItem('JwtCooper');
    if (Token) {
      this.router.navigate(['/myPage', "my"]);
    }
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  public register() {
    var body = {
      Name: this.formModel.value.UserName,
      Nickname: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password
    };
    this.http.post(this.registrationUrl, body).subscribe(
      (res: any) => {
        this.router.navigate(['/myPage', "my"]);
        },
      err => {
        console.log(err);
      }
    );
  }

  public login(credentials)
  {
    this.http.post(this.authUrl, credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.loginOK((<any>response).token);
      
    }, err => {
      this.BadLogin();
    });
  }

  public socialSignIn(socialPlatform : string) {
    this.socialAuthService.signIn(this.getProvider[socialPlatform]).then(
      (userData) => {
        if (socialPlatform == "google") {
          userData.id = userData.email;
          userData.token = userData.idToken;
        }
        this.socialsignin(userData);
      }
    );
  }

  socialsignin(userData) {
    var body = this.bodyCreator(userData, true);

    this.http.post(this.authUrl, body).subscribe(
      response => {
        this.loginOK((<any>response).token);
      },
      err => {
        if (err.error == "Auth") {
          //Register
          body = this.bodyCreator(userData, false);
          this.http.post(this.registrationUrl, body).subscribe(
            response => {
              //Try to login 1 time
              body = this.bodyCreator(userData, true);
              this.http.post(this.authUrl, body).subscribe(
                response => {
                  this.loginOK((<any>response).token);
                },
                err => {
                  this.BadLogin();
                }
              );
            }
          );
        }
        else 
        {
          this.BadLogin();
        }
      }
    );  
  }

  loginOK(token) {
    localStorage.setItem("JwtCooper", token);
    this.router.navigate(['/myPage', "my"]);
  }

  public BadLogin() {
    this.router.navigate(["/myPage", "my"]);
  }

  bodyCreator(userData, login) {
    let result;
    if (login == true)
    {
      result = {
        Username: userData.email,
        ID: userData.id,
        Provider: userData.provider,
        Password: userData.token
      };
    }
    else 
    {
      result = {
        Name: userData.name,
        Email: userData.email,
        Nickname: userData.id,
        Password: userData.token,
        Provider: userData.provider,
        PhotoURL: userData.image
      };
    }

    return result;
  }
}

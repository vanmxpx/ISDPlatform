import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { AuthService, FacebookLoginProvider, GoogleLoginProvider, LinkedinLoginProvider } from 'ng-dynami-social-login';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient,private socialAuthService: AuthService) { }
  public user: any;
  readonly registrationUrl = '/api/registration';


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
  registerFacebook() {
    this.facebookLogin();
    console.log(this.user);
    var body = {
      Email: this.user.email,
    //  Token: this.user.token,
    };
    return this.http.post(this.registrationUrl, body);
  }
  facebookLogin()
  {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then((userData)=>{this.user=userData;});
  }

  googleLogin()
  {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then((userData)=>{this.user=userData;});
  }

}

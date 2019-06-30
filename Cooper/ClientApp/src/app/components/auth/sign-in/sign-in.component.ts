import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state,} from '@angular/animations';
import { fader } from '../../../animations/route-animation';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  animations: [fader]
})
export class SignInComponent implements OnInit {

  invalidLogin: boolean;
  readonly authUrl = '/auth/login';

  constructor(private router: Router, private http: HttpClient, private service: UserService) {
    this.CheckAuthentification();
  }

  CheckAuthentification(): void {
    const Token: string = localStorage.getItem('JwtCooper');
    if (Token) {
      this.router.navigate(['/myPage', "my"]);
    }
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.http.post(this.authUrl, credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.invalidLogin = false;
      this.service.loginOK((<any>response).token, this.router);
      
    }, err => {
      this.invalidLogin = true;
      this.service.BadLogin(this.router);
    });
  }

  socialSignIn(platform) {
    this.service.socialSignIn(platform);
  }
  
ngOnInit() {}
}

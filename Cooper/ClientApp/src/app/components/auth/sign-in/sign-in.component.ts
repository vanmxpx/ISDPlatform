import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state,} from '@angular/animations';
import { fader } from '../../../animations/route-animation';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'cooper-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  animations: [fader]
})
export class SignInComponent {

  invalidLogin: boolean;


  constructor(private router: Router, private http: HttpClient) {
    this.CheckAuthentification();
  }

  CheckAuthentification(): void {

    const Token: string = localStorage.getItem('JwtCooper');
    if (Token) {
      this.router.navigate(['/myPage']);
    }
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.http.post("/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
       let token = (<any>response).token;
       localStorage.setItem("JwtCooper", token);
       this.invalidLogin = false;
      this.router.navigate(["/myPage"]);
      
    }, err => {
      this.router.navigate(["/myPage"]);
      this.invalidLogin = true;
    });
  }
ngOnInit() {}
}

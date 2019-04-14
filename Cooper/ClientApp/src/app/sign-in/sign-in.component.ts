import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state,} from '@angular/animations';
import { fader } from '../route-animation';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  animations: [fader]
})
export class SignInComponent {

  invalidLogin: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.http.post("/api/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      let token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["home"]);
      
    }, err => {
      this.invalidLogin = true;
    });
  }
ngOnInit() {}
}

import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'coop-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  failedLoginMessage = 'Invalid username or password.';

  @Output() signIn = new EventEmitter<NgForm>();
  @Output() socialSignIn = new EventEmitter<string>();
  constructor() { }

  ngOnInit() {
  }

  signInClicked(form: NgForm): void {
    this.signIn.emit(form);
  }

  socialSignInClicked(socialNetwork: string): void {
    this.socialSignIn.emit(socialNetwork);
  }
}

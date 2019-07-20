import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  failedLoginMessage = 'Invalid username or password.';

  @Input() failedLogin: boolean;
  @Output() signIn = new EventEmitter<NgForm>();
  @Output() socialSignIn = new EventEmitter<SocialNetwork>();

  constructor() { }

  ngOnInit() {
  }

  onSignInClicked(form: NgForm): void {
    this.signIn.emit(form);
  }

  onSocialSignInClicked(socialNetwork: SocialNetwork): void {
    this.socialSignIn.emit(socialNetwork);
  }
}

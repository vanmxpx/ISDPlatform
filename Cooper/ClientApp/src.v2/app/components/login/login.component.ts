import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

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

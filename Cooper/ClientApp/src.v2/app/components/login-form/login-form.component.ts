import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import {SocialNetwork} from '@enums';
import { Languages } from '@enums';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'coop-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  title = 'ngxtranslate';
  failedLoginMessage = 'Invalid username or password.';
  languages: Languages;

  @Input() failedLogin: boolean;
  @Output() signIn = new EventEmitter<NgForm>();
  @Output() socialSignIn = new EventEmitter<SocialNetwork>();

  constructor(public translate: TranslateService) { }

  ngOnInit() {
  }

  onSignInClicked(form: NgForm): void {
    this.signIn.emit(form);
  }

  onSocialSignInClicked(socialNetwork: SocialNetwork): void {
    this.socialSignIn.emit(socialNetwork);
  }
}

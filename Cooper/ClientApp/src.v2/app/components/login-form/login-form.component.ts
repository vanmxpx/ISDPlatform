import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import {SocialNetwork} from '@enums';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'coop-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {

  public failedLoginMessage: string = 'Invalid username or password.';

  @Input() public failedLogin: boolean;
  @Output() public signIn: EventEmitter<NgForm> = new EventEmitter<NgForm>();
  @Output() public socialSignIn: EventEmitter<SocialNetwork> = new EventEmitter<SocialNetwork>();

  constructor(public translate: TranslateService) { }
  public onSignInClicked(form: NgForm): void {
    this.signIn.emit(form);
  }

  public onSocialSignInClicked(socialNetwork: SocialNetwork): void {
    this.socialSignIn.emit(socialNetwork);
  }
}

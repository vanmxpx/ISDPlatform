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
  public socialNetwork: typeof SocialNetwork = SocialNetwork;
  public failedLoginMessage: string = 'Invalid username or password.';

  @Output() public settingDefaultLang: EventEmitter<string> = new EventEmitter<string>();
  @Output() public signIn: EventEmitter<NgForm> = new EventEmitter<NgForm>();
  @Output() public socialSignIn: EventEmitter<SocialNetwork> = new EventEmitter<SocialNetwork>();
  @Output() public langChangeEvent: EventEmitter<string> = new EventEmitter<string>();
  @Input() public failedLogin: boolean;
  @Input() public languageKeys: string[];
  @Input() public languages: any;
  @Input() public currentLanguage: string;
  constructor(public translate: TranslateService) { }
  public onSignInClicked(form: NgForm): void {
    this.signIn.emit(form);
  }

  public onSocialSignInClicked(socialNetwork: SocialNetwork): void {
    this.socialSignIn.emit(socialNetwork);
  }

  public onLangChanged(lang: string): void {
    this.langChangeEvent.emit(lang);
  }

  public setDefaultLang(): void {
    this.settingDefaultLang.emit(this.currentLanguage);
  }
}

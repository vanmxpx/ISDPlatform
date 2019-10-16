import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SocialNetwork } from '@enums';

@Component({
  selector: 'coop-general-form',
  templateUrl: './general-settings-form.component.html',
  styleUrls: ['./general-settings-form.component.scss']
})
export class GeneralSettingsComponent {
  public socialNetwork: typeof SocialNetwork = SocialNetwork;
  @Input() public email: string;
  @Input() public googleStatus: string;
  @Input() public facebookStatus: string;
  @Output() public changeEmail: EventEmitter<string> = new EventEmitter();
  @Output() public resetPassword: EventEmitter<string> = new EventEmitter();
  @Output() public deleteAccount: EventEmitter<string> = new EventEmitter();
  @Output() public linkSocialAccount: EventEmitter<SocialNetwork> = new EventEmitter();

  public editEmail(): void {
    console.log(this.changeEmail);
    this.changeEmail.emit();
  }
}

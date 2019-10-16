import { Component, Output, EventEmitter, Input } from '@angular/core';
import { SocialNetwork } from '@enums';

@Component({
  selector: 'coop-settings-form',
  templateUrl: './settings-form.component.html',
  styleUrls: ['./settings-form.component.scss']
})
export class SettingsComponent {
  public nowMenu: number = 0;
  @Input() public email: string;
  @Input() public googleStatus: string;
  @Input() public facebookStatus: string;
  @Output() public changeEmail: EventEmitter<string> = new EventEmitter();
  @Output() public resetPassword: EventEmitter<string> = new EventEmitter();
  @Output() public deleteAccount: EventEmitter<string> = new EventEmitter();
  @Output() public linkSocialAccount: EventEmitter<SocialNetwork> = new EventEmitter();

  public setMenu(now: number): void {
    this.nowMenu = now;
  }
}

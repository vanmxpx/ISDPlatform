import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'coop-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent {

  public failedLoginMessage: string = 'Invalid username or password.';

  @Input() public failedLogin: boolean;
  @Output() public resetPassword: EventEmitter<NgForm> = new EventEmitter<NgForm>();

  public onForgotPasswordClicked(form: NgForm): void {
    this.resetPassword.emit(form);
  }
}

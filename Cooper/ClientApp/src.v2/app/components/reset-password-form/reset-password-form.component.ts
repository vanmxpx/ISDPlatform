import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'coop-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent {

  // public failedResetMessage: string = 'Invalid email';

  @Input() public failedReset: boolean;
  @Input() public failedResetMessage: string;
  @Output() public resetPassword: EventEmitter<NgForm> = new EventEmitter<NgForm>();

  public onResetPasswordClicked(form: NgForm): void {
    this.resetPassword.emit(form);
  }
}

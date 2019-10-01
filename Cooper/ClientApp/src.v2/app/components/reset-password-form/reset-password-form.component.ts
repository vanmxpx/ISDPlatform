import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent {

  @Input() public notified: boolean;
  @Input() public failed: boolean;
  @Input() public message: string;
  @Output() public resetPassword: EventEmitter<NgForm> = new EventEmitter<NgForm>();

  constructor(public translate: TranslateService) { }

  public onResetPasswordClicked(form: NgForm): void {
    this.resetPassword.emit(form);
  }
}

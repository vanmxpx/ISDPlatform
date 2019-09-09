import { Component, Output, EventEmitter, Input } from '@angular/core';
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'coop-confirm-password-form',
  templateUrl: './confirm-password-form.component.html',
  styleUrls: ['./confirm-password-form.component.css']
})
export class ConfirmPasswordFormComponent {

  @Input() public errorMessage: string;
  @Output() public resetPassword: EventEmitter<NgForm> = new EventEmitter<NgForm>();
  @Output() public displayError: EventEmitter<any> = new EventEmitter<any>();

  private readonly emptyFieldWarning: string = 'This field is empty';
  private passwordConfirmationForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.passwordConfirmationForm = this.formBuilder.group({
      Passwords: this.formBuilder.group({
        Password: ['', [Validators.required, Validators.minLength(4)]],
        ConfirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })
    });
  }

  public onConfirmPasswordClicked(): void {
    this.resetPassword.emit(this.passwordConfirmationForm.value.Passwords.Password);
  }

  public onErrorDisplay(): void {
    this.displayError.emit(null);
  }

  public getPasswordErrorMessage(): string {
    const fieldName = 'Passwords.Password';

    if (this.passwordConfirmationForm.get(fieldName).touched) {
      if (this.passwordConfirmationForm.get(fieldName).hasError('minlength')) {
        this.onErrorDisplay();
        return 'Minimum 4 characters required!';
      } else if (this.passwordConfirmationForm.get(fieldName).hasError('required')) {
        this.onErrorDisplay();
        return this.emptyFieldWarning;
      }
    }

    return '';
  }

  public getConfirmPasswordErrorMessage(): string {
    const fieldName = 'Passwords.ConfirmPassword';

    if (this.passwordConfirmationForm.get(fieldName).touched) {

      if (this.passwordConfirmationForm.get(fieldName).hasError('required')) {
        this.onErrorDisplay();
        return this.emptyFieldWarning;
      } else if (this.passwordConfirmationForm.get(fieldName).hasError('passwordMismatch')) {
        this.onErrorDisplay();
        return 'Passwords are not the same.';
      }
    }

    return '';
  }

  private comparePasswords(passwordConfirmationGroup: FormGroup): any {
    const confirmPswrdCtrl = passwordConfirmationGroup.get('ConfirmPassword');

    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (passwordConfirmationGroup.get('Password').value !== confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }
    }
  }

}

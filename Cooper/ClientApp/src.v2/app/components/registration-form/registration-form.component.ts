import { Component, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent {

  registrationForm: FormGroup;
  emptyFieldWarning = 'This field is empty';

  @Output() signUp = new EventEmitter<any>();
  @Output() socialSignIn = new EventEmitter<SocialNetwork>();

  constructor(private formBuilder: FormBuilder) {
    this.registrationForm = this.formBuilder.group({
      Nickname: ['', Validators.required],
      Email: ['', [Validators.email, Validators.required]],

      Passwords: this.formBuilder.group({
        Password: ['', [Validators.required, Validators.minLength(4)]],
        ConfirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })
    });
  }

  private onSignUpClicked(): void {

    const body = {
      Name: this.registrationForm.value.Nickname,
      Nickname: this.registrationForm.value.Nickname,
      Email: this.registrationForm.value.Email,
      Password: this.registrationForm.value.Passwords.Password
    };

    this.signUp.emit(body);
  }

  private onSocialSignInClicked(network: SocialNetwork): void {
    this.socialSignIn.emit(network);
  }

  private getEmailErrorMessage() {

    const fieldName = 'Email';

    if (this.registrationForm.controls[fieldName].touched) {

      if (this.registrationForm.controls[fieldName].hasError('email')) {
        return 'Invalid email adress';
      } else if (this.registrationForm.controls[fieldName].hasError('required')) {
        return this.emptyFieldWarning;
      }
    }
  }

  private getNicknameErrorMessage() {
    const fieldName = 'Nickname';

    if (this.registrationForm.controls[fieldName].touched && this.registrationForm.controls[fieldName].hasError('required')) {
      return this.emptyFieldWarning;
    }
  }

  private getPasswordErrorMessage() {
    const fieldName = 'Passwords.Password';

    if (this.registrationForm.get(fieldName).touched) {

      if (this.registrationForm.get(fieldName).hasError('minlength')) {
      return 'Minimum 4 characters required!';
    } else if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
      }
    }
  }

  private getConfirmPasswordErrorMessage() {
    const fieldName = 'Passwords.ConfirmPassword';

    if (this.registrationForm.get(fieldName).touched) {

      if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
    } else if (this.registrationForm.get(fieldName).hasError('passwordMismatch')) {
      return 'Passwords are not the same.';
    }
  }
  }

  private comparePasswords(registrationForm: FormGroup) {

    const confirmPswrdCtrl = registrationForm.get('ConfirmPassword');

    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {

      if (registrationForm.get('Password').value !== confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }

    }
    }
}

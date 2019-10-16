import { Component, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent {
  public socialNetwork: typeof SocialNetwork = SocialNetwork;
  private registrationForm: FormGroup;
  private emptyFieldWarning: string = 'This field is empty';

  @Output() public signUp: EventEmitter<any> = new EventEmitter<any>();
  @Output() public socialSignIn: EventEmitter<SocialNetwork> = new EventEmitter<SocialNetwork>();

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

  public onSignUpClicked(): void {

    const body = {
      Name: this.registrationForm.value.Nickname,
      Nickname: this.registrationForm.value.Nickname,
      Email: this.registrationForm.value.Email,
      Password: this.registrationForm.value.Passwords.Password
    };

    this.signUp.emit(body);
  }

  public onSocialSignInClicked(network: SocialNetwork): void {
    this.socialSignIn.emit(network);
  }

  public getEmailErrorMessage(): string {

    const fieldName = 'Email';

    if (this.registrationForm.controls[fieldName].touched) {

      if (this.registrationForm.controls[fieldName].hasError('email')) {
        return 'Invalid email adress';
      } else if (this.registrationForm.controls[fieldName].hasError('required')) {
        return this.emptyFieldWarning;
      }
    }

    return '';
  }

  public getNicknameErrorMessage(): string {
    const fieldName = 'Nickname';

    if (this.registrationForm.controls[fieldName].touched && this.registrationForm.controls[fieldName].hasError('required')) {
      return this.emptyFieldWarning;
    }

    return '';
  }

  public getPasswordErrorMessage(): string {
    const fieldName = 'Passwords.Password';

    if (this.registrationForm.get(fieldName).touched) {

      if (this.registrationForm.get(fieldName).hasError('minlength')) {
      return 'Minimum 4 characters required!';
    } else if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
      }
    }

    return '';
  }

  public getConfirmPasswordErrorMessage(): string {
    const fieldName = 'Passwords.ConfirmPassword';

    if (this.registrationForm.get(fieldName).touched) {

      if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
    } else if (this.registrationForm.get(fieldName).hasError('passwordMismatch')) {
      return 'Passwords are not the same.';
    }
  }

    return '';
  }

  private comparePasswords(registrationForm: FormGroup): any {

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

import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { FormControl, NgForm, FormGroupDirective, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { RegistrationService } from '../../../services/registration/registration.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
    loading = false;
    submitted = false;
    registrationForm: FormGroup;
    emptyFieldWarning = 'This field is empty';

  ngOnInit() {
      this.registrationForm = this.formBuilder.group({
        Nickname: ['', Validators.required],
        Email: ['', [Validators.email, Validators.required]],
        Passwords: this.formBuilder.group({
          Password: ['', [Validators.required, Validators.minLength(4)]],
          ConfirmPassword: ['', Validators.required]
        }, { validator: this.comparePasswords })
      });
  }

  constructor(private formBuilder: FormBuilder, private userService: UserService, private service: RegistrationService) {
    this.userService.CheckAuthentification();
  }

  onSubmit() {

    const body = {
      Name: this.registrationForm.value.Nickname,
      Nickname: this.registrationForm.value.Nickname,
      Email: this.registrationForm.value.Email,
      Password: this.registrationForm.value.Passwords.Password
    };

    this.service.register(body);
  }

  getEmailErrorMessage() {

    const fieldName = 'Email';

    if (this.registrationForm.controls[fieldName].touched) {

      if (this.registrationForm.controls[fieldName].hasError('email')) {
        return 'Invalid email adress';
      } else if (this.registrationForm.controls[fieldName].hasError('required')) {
        return this.emptyFieldWarning;
      }
  }
  }

  getNicknameErrorMessage() {
    const fieldName = 'Nickname';

    if (this.registrationForm.controls[fieldName].touched && this.registrationForm.controls[fieldName].hasError('required')) {
      return this.emptyFieldWarning;
    }
  }

  getPasswordErrorMessage() {
    const fieldName = 'Passwords.Password';

    if (this.registrationForm.get[fieldName].touched) {

      if (this.registrationForm.get(fieldName).hasError('minlength')) {
      return 'Minimum 4 characters required!';
    } else if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
    }
  }
  }

  getConfirmPasswordErrorMessage() {
    const fieldName = 'Passwords.ConfirmPassword';

    if (this.registrationForm.get[fieldName].touched) {

      if (this.registrationForm.get(fieldName).hasError('required')) {
      return this.emptyFieldWarning;
    } else if (this.registrationForm.get(fieldName).hasError('passwordMismatch')) {
      return 'Passwords are not the same.';
    }
  }

  }

  comparePasswords(registrationForm: FormGroup) {

    const confirmPswrdCtrl = registrationForm.get('ConfirmPassword');

    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {

      if (registrationForm.get('Password').value !== confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }

    }
  }

  socialSignIn(platform) {
    this.userService.socialSignIn(platform);
  }
}

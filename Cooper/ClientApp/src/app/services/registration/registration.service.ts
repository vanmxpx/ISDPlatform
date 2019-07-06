import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  readonly registrationUrl = '/registration';
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],

    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  constructor(private fb: FormBuilder, private router: Router, private http: HttpClient) { }

  public register() {

    const body = {
      Name: this.formModel.value.UserName,
      Nickname: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password
    };

    this.http.post(this.registrationUrl, body).subscribe(
      (res: any) => {
        this.router.navigate(['/myPage', 'my']);
        },
      err => {
        console.log(err);
      }
    );
    }

    comparePasswords(fb: FormGroup) {

      const confirmPswrdCtrl = fb.get('ConfirmPassword');

      if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {

        if (fb.get('Password').value !== confirmPswrdCtrl.value) {
          confirmPswrdCtrl.setErrors({ passwordMismatch: true });
        } else {
          confirmPswrdCtrl.setErrors(null);
        }

      }
    }


}

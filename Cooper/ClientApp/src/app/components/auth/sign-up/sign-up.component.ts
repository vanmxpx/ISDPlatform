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
    registerForm: FormGroup;
    loading = false;
    submitted = false;

  constructor(private userService: UserService, private service: RegistrationService) {
    this.userService.CheckAuthentification();
  }

  onSubmit() {
    this.service.register();
  }

  socialSignIn(platform) {
    this.userService.socialSignIn(platform);
  }

  ngOnInit() {
    this.service.formModel.reset();
  }
}

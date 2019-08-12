import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { UserService, RegistrationService, AuthentificationService } from '@services';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-registration-layout',
  templateUrl: './registration.layout.html',
  styleUrls: ['./registration.layout.css']
})

export class RegistrationLayoutComponent {

  constructor(private formBuilder: FormBuilder, private userService: UserService, private service: RegistrationService,
              private authService: AuthentificationService, private router: Router) {
    if (this.authService.isAuthentificated()) {
      this.router.navigate(['/platform/home']);
    }
  }

  private registerUser(body: any) {
    this.service.register(body);
  }

  private socialSignIn(platform: SocialNetwork) {
    this.authService.socialSignIn(platform);
  }
}

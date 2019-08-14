import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService, AuthentificationService } from '@services';
import {SocialNetwork} from '@enums';

@Component({
  selector: 'coop-registration-layout',
  templateUrl: './registration.layout.html',
  styleUrls: ['./registration.layout.css']
})

export class RegistrationLayoutComponent {

  constructor(private service: RegistrationService,
              private authService: AuthentificationService, private router: Router) {
    if (this.authService.isAuthentificated()) {
      this.router.navigate(['/platform/home']);
    }
  }

  public registerUser(body: any): void {
    this.service.register(body);
  }

  public socialSignIn(platform: SocialNetwork): void {
    this.authService.socialSignIn(platform);
  }
}

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService, AuthentificationService } from '@services';
import {SocialNetwork} from '@enums';
import { MatDialog } from '@angular/material/dialog';
import { SocialRegisterComponent } from '@components';

@Component({
  selector: 'coop-registration-layout',
  templateUrl: './registration.layout.html',
  styleUrls: ['./registration.layout.css']
})

export class RegistrationLayoutComponent {

  constructor(private service: RegistrationService,
              private authService: AuthentificationService, private router: Router, public dialog: MatDialog) {
    if (this.authService.isAuthentificated()) {
      this.router.navigate(['/platform/home']);
    }

    this.authService.registerEvent.subscribe(
      (userData) => {
        const dialogRef = this.dialog.open(SocialRegisterComponent, {
          data: { type: 'avatar' }
        });
        dialogRef.afterClosed().subscribe(
          (nickname) => {
            if (nickname !== '') {
              this.authService.socialRegister(userData, nickname);
            }
          });
      }
    );
  }

  public registerUser(body: any): void {
    this.service.register(body);
  }

  public socialSignIn(platform: SocialNetwork): void {
    this.authService.socialSignIn(platform);
  }
}

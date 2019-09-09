import { Component } from '@angular/core';
import { /*AuthentificationService,*/ ResetPasswordService } from '@services';
import { ActivatedRoute, /*Router*/ } from '@angular/router';

@Component({
  selector: 'coop-confirm-password-layout',
  templateUrl: './confirm-password.layout.html',
  styleUrls: ['./confirm-password.layout.css']
})
export class ConfirmPasswordLayoutComponent {

  private token: string;

  constructor(private resetPasswordService: ResetPasswordService, private route: ActivatedRoute) {
    this.route.params.subscribe((params) => {
      if (params.token) {
        this.token = params.token;
      } else {
        this.token = '';
      }
    });
  }

  public resetPassword(newPassword: string): void {
    console.log('token: ' + this.token);
    console.log('new password: ' + newPassword);
    if (this.token && newPassword) {
      this.resetPasswordService.resetPassword(this.token, newPassword);
    }
  }
}

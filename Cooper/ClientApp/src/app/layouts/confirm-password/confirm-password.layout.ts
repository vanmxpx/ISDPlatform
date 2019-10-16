import { Component, OnDestroy } from '@angular/core';
import { ResetPasswordService } from '@services';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'coop-confirm-password-layout',
  templateUrl: './confirm-password.layout.html',
  styleUrls: ['./confirm-password.layout.css']
})
export class ConfirmPasswordLayoutComponent implements OnDestroy {

  private token: string;
  private subscriptions: Subscription;
  public errorMessage: string;

  constructor(private router: Router, private resetPasswordService: ResetPasswordService, private route: ActivatedRoute) {
    this.subscriptions = new Subscription();
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
      this.subscriptions.add(this.resetPasswordService.resetPassword(this.token, newPassword).subscribe(() => {
        console.log('Password was reset successfully.');
        this.router.navigate(['/login']);
      },
        (err) => {
          console.log('Password was not reset.');
          console.log('Error: ', err);
          this.errorMessage = err.error;
          console.log(this.errorMessage);
        }));
    }
  }

  public displayError(_: any): void {
    setTimeout(() => {
      this.errorMessage = '';
    }, 0);
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}

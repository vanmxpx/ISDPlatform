import { Component, OnInit } from '@angular/core';
import { AuthentificationService, ResetPasswordService } from '@services';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'coop-reset-password-layout',
  templateUrl: './reset-password.layout.html',
  styleUrls: ['./reset-password.layout.css']
})
export class ResetPasswordLayoutComponent implements OnInit {

  public failedLogin: boolean = false;

  constructor(private authService: AuthentificationService, private resetPasswordService: ResetPasswordService,
              private route: ActivatedRoute, private router: Router) { }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log(params);
      if (params.failedLogin) {
        this.failedLogin = params.failedLogin;
      }
    });

    if (this.authService.isAuthentificated()) {
      this.router.navigate(['/platform/home']);
    }
  }

  public resetPassword(form: NgForm): void {
    this.resetPasswordService.send(form.value.email);
  }
}

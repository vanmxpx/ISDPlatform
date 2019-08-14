import { Component, OnInit } from '@angular/core';
import { AuthentificationService } from '@services';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'coop-reset-password-layout',
  templateUrl: './reset-password.layout.html',
  styleUrls: ['./reset-password.layout.css']
})
export class ResetPasswordLayoutComponent implements OnInit {

  public failedLogin: boolean = false;

  constructor(private authService: AuthentificationService, private route: ActivatedRoute, private router: Router) { }

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

  public forgotPassword(form: NgForm): void {
    console.log(form.value.email);
  }
}

import { Component, OnInit } from '@angular/core';
import { ResetPasswordService } from '@services';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'coop-reset-password-layout',
  templateUrl: './reset-password.layout.html',
  styleUrls: ['./reset-password.layout.css']
})
export class ResetPasswordLayoutComponent implements OnInit {

  public failedReset: boolean = false;
  public failedResetMessage: string;

  constructor(private resetPasswordService: ResetPasswordService, private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log(params);
      if (params.failedReset) {
        this.failedReset = params.failedReset;
        this.failedResetMessage = params.failed;
      } else {
        this.failedReset = false;
      }
    });
  }

  public resetPassword(form: NgForm): void {
    console.log(form.value.email);
    this.resetPasswordService.send(form.value.email);
  }
}

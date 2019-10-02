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

  public notified: boolean;
  public failed: boolean;
  public message: string;

  constructor(private resetPasswordService: ResetPasswordService, private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params.failed) {
        this.failed = params.failed;
        this.message = params.msg;
      } else {
        this.failed = false;
      }

      if (params.notified) {
        this.notified = params.notified;
        this.message = params.msg;
      } else {
        this.notified = false;
      }
    });
  }

  public resetPassword(form: NgForm): void {
    console.log(form.value.email);
    this.resetPasswordService.send(form.value.email);
  }
}

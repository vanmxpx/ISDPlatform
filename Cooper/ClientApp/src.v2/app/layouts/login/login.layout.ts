import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { AuthentificationService } from '@services/authentification/authentification.service';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger, transition, style, query, group, animateChild, animate, keyframes, state} from '@angular/animations';
// import { fader } from '../../../animations/route-animation';
import { NgForm } from '@angular/forms';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'coop-login-layout',
  templateUrl: './login.layout.html',
  styleUrls: ['./login.layout.css']
})
export class LoginLayoutComponent implements OnInit {

  failedLogin = false;

  constructor(private authService: AuthentificationService, private route: ActivatedRoute) {

    this.route.params.subscribe(params => {
      console.log(params);
      if (params['failedLogin']) {
        this.failedLogin = params['failedLogin'];
      }
    });

    this.authService.checkAuthentification();
  }

  signIn(form: NgForm): void {
    this.authService.login(JSON.stringify(form.value));
  }

  socialSignIn(platform: string): void {
    this.authService.socialSignIn(platform);
  }

  ngOnInit() {
  }

}

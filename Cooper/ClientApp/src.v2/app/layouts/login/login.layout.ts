import { Component } from '@angular/core';
import { AuthentificationService } from '@services';
// import { fader } from '../../../animations/route-animation';
import { NgForm } from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {SocialNetwork} from '@enums';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-login-layout',
  templateUrl: './login.layout.html',
  styleUrls: ['./login.layout.css']
})
export class LoginLayoutComponent {

  public failedLogin: boolean = false;

  constructor(private authService: AuthentificationService, private route: ActivatedRoute,
              private router: Router, public translate: TranslateService) {

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

  public signIn(form: NgForm): void {
    this.authService.signIn(JSON.stringify(form.value));
  }

  public socialSignIn(platform: SocialNetwork): void {
    this.authService.socialSignIn(platform);
  }

}

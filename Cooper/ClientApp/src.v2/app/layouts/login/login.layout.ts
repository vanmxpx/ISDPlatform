import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { UserService } from '@services/model-services/user.service';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger, transition, style, query, group, animateChild, animate, keyframes, state} from '@angular/animations';
// import { fader } from '../../../animations/route-animation';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'coop-login-layout',
  templateUrl: './login.layout.html',
  styleUrls: ['./login.layout.css']
})
export class LoginLayoutComponent implements OnInit {

  constructor(private userService: UserService) {
    this.userService.checkAuthentification();
  }

  signIn(form: NgForm): void {
    this.userService.login(JSON.stringify(form.value));
  }

  socialSignIn(platform: string): void {
    this.userService.socialSignIn(platform);
  }

  ngOnInit() {
  }

}

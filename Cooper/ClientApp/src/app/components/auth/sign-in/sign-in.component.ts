import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state,} from '@angular/animations';
import { fader } from '../../../animations/route-animation';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  animations: [fader]
})
export class SignInComponent implements OnInit {

  constructor(private service: UserService) {
    this.service.CheckAuthentification();
  }

  login(form: NgForm) {
    this.service.login(JSON.stringify(form.value));
  }

  socialSignIn(platform) {
    this.service.socialSignIn(platform);
  }
  
ngOnInit() {}
}

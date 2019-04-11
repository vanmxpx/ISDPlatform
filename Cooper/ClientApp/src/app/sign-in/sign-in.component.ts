import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state,} from '@angular/animations';
import { fader } from '../route-animation';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  animations: [fader]
})
export class SignInComponent implements OnInit {

  constructor() { }

  ngOnInit() {}
  
  
}

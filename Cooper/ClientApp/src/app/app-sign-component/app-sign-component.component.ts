import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {fader, logoAnimation, joystickAnimation, panelAnimation} from '../route-animation';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state} from '@angular/animations';

@Component({
  selector: 'app-app-sign-component',
  animations: [ logoAnimation, joystickAnimation, trigger('routeAnimations', [
    state('void', style({
     opacity: 0
   })),
   transition('void <=> *', animate(1000)),
 ]), panelAnimation],
  templateUrl: './app-sign-component.component.html',
  styleUrls: ['./app-sign-component.component.css']
})
export class AppSignComponentComponent implements OnInit {

  prepareRoute(outlet: RouterOutlet) {return outlet.activatedRouteData['animation'];}

  constructor() { }

  ngOnInit() {

  }

}

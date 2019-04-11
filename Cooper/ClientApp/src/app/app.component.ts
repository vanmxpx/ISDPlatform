import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {fader, logoAnimation, joystickAnimation, panelAnimation} from './route-animation';
import {trigger,transition, style, query,group,animateChild, animate, keyframes, state} from '@angular/animations';

@Component({
  selector: 'app-root',
  animations: [ logoAnimation, joystickAnimation, trigger('routeAnimations', [
    state('void', style({
     opacity: 0
   })),
   transition('void <=> *', animate(1000)),
 ]), panelAnimation],
 templateUrl: './app.component.html',
 styleUrls: ['./app.component.css']
})

export class AppComponent {
  prepareRoute(outlet: RouterOutlet) {
    return outlet.activatedRouteData['animation'];
  }

  constructor(){
  }
}

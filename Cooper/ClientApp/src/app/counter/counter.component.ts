import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router } from '@angular/router';
@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {
  }

    isUserAuthenticated() {
      let token: string = localStorage.getItem("jwt");
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        return true;
      }
      else {
        return false;
      }
    
  } 
  logOut() {
    localStorage.removeItem("jwt");
  }
   public incrementCounter() {
    this.currentCount++;
 }
  public currentCount = 0;

 
}

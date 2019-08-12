import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    const token: String = localStorage.getItem("JwtCooper");

    if (token) {
      return true;
    }
    this.router.navigate(['/auth'], { queryParams: { returnUrl: state.url }});

    return false;
  }
}

import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("JwtCooper");

    if (token != null)
    {
      return true;
    }
    this.router.navigate(['/signIn'], { queryParams: { returnUrl: state.url } });

    return false;
  }
}

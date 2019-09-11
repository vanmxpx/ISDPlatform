import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }
  public canActivate(_: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const token: string = localStorage.getItem('JwtCooper');

    if (token) {
      return true;
    }
    this.router.navigate(['/auth'], { queryParams: { returnUrl: state.url }});

    return false;
  }
}

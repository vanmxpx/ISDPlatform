import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }

  // tslint:disable-next-line: variable-name
  public canActivate(_: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const token: string = localStorage.getItem('JwtCooper');

    if (token) {
      return true;
    }
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});

    return false;
  }
}

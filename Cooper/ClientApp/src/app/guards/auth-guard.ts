import { JwtHelperService } from '@auth0/angular-jwt'
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      console.log(this.jwtHelper.decodeToken(token));
      return true;
    }
    //this.router.navigate(['/signIn'], { queryParams: { returnUrl: state.url }});// строка перебрасывает нас на страничку логирования, если пользователь не залогинен
    // Это происходит в идеале. на практике же, почему- то постоянно проходит проверка. Поэтому не работает
    return false;
  }
}

import { JwtHelperService } from '@auth0/angular-jwt';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'cooper-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  constructor( private router: Router) {
  }

  ToGames(): void
  {
    this.router.navigate(["game"]);
  }

}

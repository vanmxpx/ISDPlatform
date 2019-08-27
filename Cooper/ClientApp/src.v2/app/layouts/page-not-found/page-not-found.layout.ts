import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'coop-page-not-found-layout',
  templateUrl: './page-not-found.layout.html',
  styleUrls: ['./page-not-found.layout.css']
})
export class PageNotFoundLayoutComponent {

  constructor(private router: Router) {
  }

  public goToTheHomePage(): void {
    this.router.navigate(['/platform/home/']);
  }
}

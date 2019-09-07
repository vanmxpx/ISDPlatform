import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {SessionService} from '@services';

@Component({
  selector: 'coop-page-not-found-layout',
  templateUrl: './page-not-found.layout.html',
  styleUrls: ['./page-not-found.layout.css']
})
export class PageNotFoundLayoutComponent {

  constructor(private sessionService: SessionService, private router: Router) {
  }

 public goToProfilePage(): void {
   this.router.navigate(['/platform/profile/' + this.sessionService.GetSessionUserNickname()]);
 }
}

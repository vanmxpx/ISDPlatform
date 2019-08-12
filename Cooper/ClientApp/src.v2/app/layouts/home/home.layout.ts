import { Component } from '@angular/core';
import {SessionService} from '@services';
import {Router} from '@angular/router';

@Component({
  selector: 'coop-home',
  templateUrl: './home.layout.html',
  styleUrls: ['./home.layout.scss']
})
export class HomeLayoutComponent {

  constructor(private sessionService: SessionService, private router: Router) {
   }

  public goToProfilePage(): void {
    this.router.navigate(['/platform/profile/' + this.sessionService.GetSessionUserNickname()]);
  }
}

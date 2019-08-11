import { Component, OnInit } from '@angular/core';
import {SessionService} from '@services';
import {Router} from '@angular/router';

@Component({
  selector: 'coop-home',
  templateUrl: './home.layout.html',
  styleUrls: ['./home.layout.scss']
})
export class HomeLayoutComponent implements OnInit {


  constructor(private sessionService: SessionService, private router: Router) {
   }


  ngOnInit() {
  }

  goToProfilePage() {
    this.router.navigate(['/platform/profile/' + this.sessionService.GetSessionUserNickname()]);
  }
}

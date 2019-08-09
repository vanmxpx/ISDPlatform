import { Component, OnInit, Input } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {


  constructor() { }

  @Input() profile: User;

  ngOnInit() {
  }

}

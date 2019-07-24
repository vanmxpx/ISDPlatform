import { Component, OnInit, Input } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-foreign-profile',
  templateUrl: './foreign-profile.component.html',
  styleUrls: ['./foreign-profile.component.css']
})
export class ForeignProfileComponent implements OnInit {


  constructor() { }

  @Input() profile: User;

  ngOnInit() {
  }

}

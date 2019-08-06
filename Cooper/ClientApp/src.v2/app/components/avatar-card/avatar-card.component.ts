import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'coop-avatar-card',
  templateUrl: './avatar-card.component.html',
  styleUrls: ['./avatar-card.component.scss']
})
export class AvatarCardComponent implements OnInit {

  constructor() { }

  @Input() nickname: string;
  @Input() photoURL: string;

  ngOnInit() {
  }

}

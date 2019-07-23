import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-user-interaction-list',
  templateUrl: './user-interaction-list.component.html',
  styleUrls: ['./user-interaction-list.component.css']
})
export class UserInteractionListComponent implements OnInit {

  @Input() friends: User[];
  @Input() subscribers: User[];
  @Input() subscriptions: User[];

  @Input() friendsAmount: User[];
  @Input() subscribersAmount: User[];
  @Input() subscriptionsAmount: User[];

  @Output() userChosen = new EventEmitter<string>(true);

  constructor() { }

  ngOnInit() {
  }

  onInListUserClicked(nickname: string): void {
    this.userChosen.emit(nickname);
  }

}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-user-connections-list',
  templateUrl: './user-connections-list.component.html',
  styleUrls: ['./user-connections-list.component.css']
})
export class UserConnectionsListComponent implements OnInit {

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

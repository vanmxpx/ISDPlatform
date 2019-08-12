import { Component, Input, Output, EventEmitter } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-user-connections-list',
  templateUrl: './user-connections-list.component.html',
  styleUrls: ['./user-connections-list.component.css']
})
export class UserConnectionsListComponent {

  @Input() public friends: User[];
  @Input() public subscribers: User[];
  @Input() public subscriptions: User[];

  @Input() public friendsAmount: User[];
  @Input() public subscribersAmount: User[];
  @Input() public subscriptionsAmount: User[];

  @Output() public userChosen: EventEmitter<string> = new EventEmitter<string>(true);

  public onInListUserClicked(nickname: string): void {
    this.userChosen.emit(nickname);
  }

}

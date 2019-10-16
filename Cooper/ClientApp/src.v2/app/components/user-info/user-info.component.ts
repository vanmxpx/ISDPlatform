import { Component, Input, Output, EventEmitter } from '@angular/core';
import {User} from '@models';

@Component({
  selector: 'coop-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent {

  @Input() public profile: User;
  @Input() public isSubscription: boolean;
  @Output() public subscribe: EventEmitter<number> = new EventEmitter<number>();
  @Output() public unsubscribe: EventEmitter<number> = new EventEmitter<number>();

  public onSubscribeButtonClicked(): void {
    this.subscribe.emit(this.profile.id);
    this.isSubscription = true;
  }

  public onUnsubscribeButtonClicked(): void {
    this.unsubscribe.emit(this.profile.id);
    this.isSubscription = false;
  }
}

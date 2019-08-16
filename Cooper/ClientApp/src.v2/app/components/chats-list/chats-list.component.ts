import { Component, Output, EventEmitter, Input } from '@angular/core';
import {DummyChat} from '@models';

@Component({
  selector: 'coop-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.scss']
})
export class ChatsListComponent {
  @Input() public chatsList: DummyChat[];
  @Output() public openModalWindow: EventEmitter<void> = new EventEmitter<void>();

  public onNewMessageClicked(): void {
    this.openModalWindow.emit();
  }
}

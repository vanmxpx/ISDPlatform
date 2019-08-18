import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import {Chat} from '@models';

@Component({
  selector: 'coop-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.scss']
})
export class ChatsListComponent implements OnInit {
  @Input() public chatsList: Chat[];
  @Output() public openModalWindow: EventEmitter<void> = new EventEmitter<void>();
  @Output() public openChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public onNewMessageClicked(): void {
    this.openModalWindow.emit();
  }

  public ngOnInit(): void {
    if (this.chatsList.length > 0) {
      this.openChat.emit(this.chatsList[0]);
    }
  }

  public onChatItemClick(chat: Chat): void {
    this.openChat.emit(chat);
  }
}

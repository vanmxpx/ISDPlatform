import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import {Chat} from '@models';

const fieldPermissibleMaxLength: number = 30;

@Component({
  selector: 'coop-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.scss']
})
export class ChatsListComponent implements OnInit {

  @Input() public currentSessionUserId: number;
  @Input() public newMessageBlockOpened: boolean;
  @Input() public chatsList: Chat[];
  @Output() public loadChat: EventEmitter<Chat> = new EventEmitter<Chat>();
  @Output() public closeNewMessageBlock: EventEmitter<void> = new EventEmitter<void>();

  public ngOnInit(): void {
    setTimeout(() => {
    if (this.chatsList && this.chatsList.length > 0) {
      this.loadChat.emit(this.chatsList[0]);
    }
    }, 1000);

  }

  public onChatItemClick(chat: Chat): void {

    this.closeNewMessageBlock.emit();

    this.loadChat.emit(chat);
  }

  public getChatName(chat: Chat): string {

    let chatHeader = chat.chatName;

    if (chatHeader.length > fieldPermissibleMaxLength) {
      chatHeader = chatHeader.slice(0, fieldPermissibleMaxLength - 3) + '...';
      return chatHeader;
    } else {
      return chatHeader;
    }
  }

  public getLastMessageInChat(chat: Chat): string {
    let messageContent: string = chat.messages[chat.messages.length - 1].content;

    if (messageContent.length > fieldPermissibleMaxLength) {
      messageContent = messageContent.slice(0, fieldPermissibleMaxLength - 3) + '...';
      return messageContent;
    } else {
      return messageContent;
    }
  }
}

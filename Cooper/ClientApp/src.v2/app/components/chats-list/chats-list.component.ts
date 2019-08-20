import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import {Chat} from '@models';

const fieldLength: number = 30;

@Component({
  selector: 'coop-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.scss']
})
export class ChatsListComponent implements OnInit {
  @Input() public currentUserId: number;
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

  public getUserImage(chat: Chat): string {

    if (chat.isOnetoOneChat) {
      return (this.currentUserId === chat.participants[0].id) ? chat.participants[1].photoURL : chat.participants[0].photoURL;
    } else {
      return chat.chatPhotoURL;

    }
  }

  public showChatName(chat: Chat): string {

    let chatHeader: string;
    if (chat.isOnetoOneChat) {
      chatHeader = (this.currentUserId === chat.participants[0].id) ?
      chat.participants[1].name : chat.participants[0].name;
    } else {
      chatHeader = (chat.chatName) ? chat.chatName : 'Non-named chat';
    }

    if (chatHeader.length > fieldLength) {
      chatHeader = chatHeader.slice(0, fieldLength - 3) + '...';
      return chatHeader;
    } else {
      return chatHeader;
    }
  }

  public showLastMessageInChat(chat: Chat): string {
    let messageContent: string = chat.messages[chat.messages.length - 1].content;

    if (messageContent.length > fieldLength) {
      messageContent = messageContent.slice(0, fieldLength - 3) + '...';
      return messageContent;
    } else {
      return messageContent;
    }
  }

}

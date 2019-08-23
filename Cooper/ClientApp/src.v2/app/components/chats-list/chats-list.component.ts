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
  @Input() public chatsList: Chat[];
  @Output() public openModalWindow: EventEmitter<void> = new EventEmitter<void>();
  @Output() public loadChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public ngOnInit(): void {
    if (this.chatsList.length > 0) {
      this.loadChat.emit(this.chatsList[0]);
    }
  }

  public onNewMessageButtonClicked(): void {
    this.openModalWindow.emit();
  }

  public onChatItemClick(chat: Chat): void {
    this.loadChat.emit(chat);
  }

  public getChatImage(chat: Chat): string {

    if (chat.isOnetoOneChat) {
      return (this.currentSessionUserId === chat.participants[0].id) ? chat.participants[1].photoURL : chat.participants[0].photoURL;
    } else {
      return chat.chatPhotoURL;

    }
  }

  public getChatName(chat: Chat): string {

    let chatHeader: string;
    if (chat.isOnetoOneChat) {

      chatHeader = (this.currentSessionUserId === chat.participants[0].id) ?
      (chat.participants[1].name !== '' ? chat.participants[1].name : chat.participants[1].nickname) :
      (chat.participants[0].name !== '' ? chat.participants[0].name : chat.participants[0].nickname);

    } else {
      chatHeader = (chat.chatName) ? chat.chatName : 'Non-named chat';
    }

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

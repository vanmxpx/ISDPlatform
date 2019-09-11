import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import {Chat} from '@models';

const fieldPermissibleMaxLength: number = 30;

@Component({
  selector: 'coop-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.scss']
})
export class ChatsListComponent implements OnInit {

  public monthNames: string[] = [
    'Jan', 'Feb', 'Mar',
    'Apr', 'May', 'Jun', 'Jul',
    'Aug', 'Sep', 'Oct',
    'Nov', 'Dec'
  ];

  public dayNames: string[] = [
    'Mon', 'Tue', 'Wed',
    'Thu', 'Fri', 'Sat', 'Sun'
  ];

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

  public getChatImage(chat: Chat): string {

      return (this.currentSessionUserId === chat.participants[0].id) ? chat.participants[1].photoURL : chat.participants[0].photoURL;

  }

  public getChatName(chat: Chat): string {

    let chatHeader = (this.currentSessionUserId === chat.participants[0].id) ?
      (chat.participants[1].name !== '' ? chat.participants[1].name : chat.participants[1].nickname) :
      (chat.participants[0].name !== '' ? chat.participants[0].name : chat.participants[0].nickname);

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

  public getLastMessageSentTime(chat: Chat): string {

    const date: Date = new Date(Date.parse(chat.messages[chat.messages.length - 1].createDate.toString()));
    const dateNow: Date = new Date();
    let messageTime: string;

    if (dateNow.getFullYear() !== date.getFullYear()) {
      const year: string = (date.getFullYear() % 100).toString();
      const month: string = date.getMonth().toString();
      const dayOfMonth: string = date.getDate().toString();

      messageTime = `${dayOfMonth}.${month}.${year}`;

    } else if (dateNow.getMonth() !== date.getMonth() ||
                dateNow.getDate() - date.getDate() > 6 ||
                dateNow.getDay() < date.getDay()) {

      const month: string = this.monthNames[date.getMonth()];
      const dayOfMonth: string = date.getDate().toString();

      messageTime = `${month} ${dayOfMonth}`;

    } else if (dateNow.getDay() !== date.getDay()) {

      const dayOfWeek: string = this.dayNames[date.getDay() - 1];

      messageTime = `${dayOfWeek}`;

    } else {
      let hours: string = date.getHours().toString();
      let minutes: string = date.getMinutes().toString();

      if (hours.length === 1) {
        hours = '0' + hours;
      }

      if (minutes.length === 1) {
        minutes = '0' + minutes;
      }

      messageTime = `${hours}:${minutes}`;
    }

    return messageTime;
  }
}

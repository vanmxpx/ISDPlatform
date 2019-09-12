import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Chat, Message, User } from '@models';

@Component({
  selector: 'coop-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent implements OnInit {

  public emptyMessageFieldError: boolean = false;
  public emptyMessageErrorContent: string = 'Please, enter your message first';

  public emptyNicknameFieldError: boolean = false;
  public emptyNicknameErrorContent: string = 'Please, point message recipient before sending a message';

  public userNotFoundErrorContent: string = 'User is not found';

  public username: string;
  public messageContent: string;

  @Input() public currentSessionUserId: number;
  @Input() public chat: Chat;
  @Input() public newMessageBlockOpened: boolean;
  @Input() public userNotFoundError: boolean;
  @Output() public sendMessage: EventEmitter<Message> = new EventEmitter<Message>();
  @Output() public createChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public ngOnInit(): void {
    setTimeout(() => {
      if (this.chat) {
      this.updateMessagesScrollBar(); } }, 2000);
  }

  private updateMessagesScrollBar(): void {
      const objDiv = document.getElementById('chatList') as HTMLElement;
      objDiv.scrollTop = objDiv.scrollHeight;
  }

  public getSenderAvatar(message: Message): string {
    for (const user of this.chat.participants) {
      if (user.id === message.senderId) {
        return user.photoURL;
      }
    }

    return '';
  }

  public getMessageDate(message: Message): string {
    const date: Date = new Date(Date.parse(message.createDate.toString()));

    const hours: number = (date.getHours() === 12) ? date.getHours() : date.getHours() % 12;
    let minutes: string = date.getMinutes().toString();
    let seconds: string = date.getSeconds().toString();

    const partOfDay: string = (date.getHours() < 13) ? 'AM' : 'PM';

    if (minutes.length === 1) {
      minutes = '0' + minutes;
    }

    if (seconds.length === 1) {
      seconds = '0' + seconds;
    }

    const result: string = `${hours}:${minutes}:${seconds} ${partOfDay}`;

    return result;
  }

  public getSenderNickname(message: Message): string {
    for (const user of this.chat.participants) {
      if (user.id === message.senderId) {
        return user.nickname;
      }
    }

    return '';
  }

  public hideErrors(): void {
    this.emptyMessageFieldError = false;
    this.emptyNicknameFieldError = false;
    this.userNotFoundError = false;
  }

  public createMessage(): void {

    this.hideErrors();

    const message: Message = new Message();
    message.content = this.messageContent.trimRight();
    message.senderId = this.currentSessionUserId;
    message.isRead = false;
    message.createDate = new Date();

    if (this.newMessageBlockOpened) {

      if (!this.messageContent) {
        this.emptyMessageFieldError = true;
        return;
      } else if (!this.username) {
        this.emptyNicknameFieldError = true;
        return;
      }

      const chat: Chat = new Chat();

      chat.messages = [];
      chat.messages.push(message);

      const user: User = new User();
      user.nickname = this.username;
      chat.participants = [];
      chat.participants.push(user);

      this.createChat.emit(chat);

      this.username = '';

    } else {

      message.chatId = this.chat.id;
      this.sendMessage.emit(message);

      setTimeout(() => {
        this.updateMessagesScrollBar(); }, 1000);
    }

    this.messageContent = '';
  }

  public getCompanionImage(): string {
    return (this.currentSessionUserId === this.chat.participants[0].id) ?
     this.chat.participants[1].photoURL : this.chat.participants[0].photoURL;

  }

  public getCompanionName(): string {
    return (this.currentSessionUserId === this.chat.participants[0].id) ?
    (this.chat.participants[1].name !== '' ? this.chat.participants[1].name : this.chat.participants[1].nickname) :
    (this.chat.participants[0].name !== '' ? this.chat.participants[0].name : this.chat.participants[0].nickname);
  }
}

import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Chat, Message, User } from '@models';

@Component({
  selector: 'coop-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent implements OnInit {

  public messageContent: string;

  public emptyMessageFieldError: boolean = false;
  public emptyMessageErrorContent: string = 'Please, enter your message first';

  public emptyNicknameFieldError: boolean = false;
  public emptyNicknameErrorContent: string = 'Please, point message recipient before sending a message';

  public notFoundUserError: boolean = false;
  public notFoundUserErrorContent: string = 'User is not found';

  @Input() public currentSessionUserId: number;
  @Input() public chat: Chat;
  @Input() public newMessageBlockOpened: boolean;
  @Input() public visibility: boolean;
  @Output() public sendMessage: EventEmitter<Message> = new EventEmitter<Message>();
  @Output() public createChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public ngOnInit(): void {
    setTimeout(() => {
      const objDiv = document.getElementById('chatList') as HTMLElement;
      objDiv.scrollTop = objDiv.scrollHeight; }, 1000);
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

    const hours: number = date.getHours() % 13;
    const minutes: number = date.getMinutes();
    const seconds: number = date.getSeconds();

    const partOfDay: string = (date.getHours() < 13) ? 'AM' : 'PM';

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
    this.notFoundUserError = false;
  }

  public createMessage(messageContent: string): void {

    this.hideErrors();

    const message: Message = new Message();
    message.content = messageContent.trimRight();
    message.senderId = this.currentSessionUserId;
    message.isRead = false;
    message.createDate = new Date();

    if (this.newMessageBlockOpened) {
      const username = (document.getElementById('nickname') as HTMLInputElement).value;

      if (!messageContent) {
        this.emptyMessageFieldError = true;
        return;
      } else if (!username) {
        this.emptyNicknameFieldError = true;
        return;
      }

      const chat: Chat = new Chat();

      chat.messages = [];
      chat.messages.push(message);

      const user: User = new User();
      user.nickname = username;
      chat.participants = [];
      chat.participants.push(user);

      this.createChat.emit(chat);

      setTimeout(() => {if (!this.visibility) {
        this.hideErrors();
      } else {
        this.notFoundUserError = true;
      } }, 2000);

    } else {

      message.chatId = this.chat.id;
      this.sendMessage.emit(message);
    }

    (document.getElementById('box') as HTMLInputElement).value = '';
  }
}

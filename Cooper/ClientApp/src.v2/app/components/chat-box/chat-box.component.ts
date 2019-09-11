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

  public userNotFoundErrorContent: string = 'User is not found';

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
    this.userNotFoundError = false;
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

    } else {

      message.chatId = this.chat.id;
      this.sendMessage.emit(message);

      setTimeout(() => {
        this.updateMessagesScrollBar(); }, 1000);
    }

    (document.getElementById('box') as HTMLInputElement).value = '';
  }
}

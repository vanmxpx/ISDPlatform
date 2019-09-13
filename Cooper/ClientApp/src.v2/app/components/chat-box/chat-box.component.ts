import { Component, Input, Output, EventEmitter, OnInit, OnChanges } from '@angular/core';
import { Chat, Message, User } from '@models';

@Component({
  selector: 'coop-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent implements OnInit, OnChanges {

  public emptyMessageFieldError: boolean = false;
  public emptyNicknameFieldError: boolean = false;

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
        this.updateMessagesScrollBar();
      }}, 2000);
  }

  public ngOnChanges(): void {
    setTimeout(() => {
    if (this.chat) {
      this.updateMessagesScrollBar();
    }}, 200);
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
    message.senderId = this.currentSessionUserId;
    message.isRead = false;
    message.createDate = new Date();

    if (this.newMessageBlockOpened) {

      if (!this.username) {
        this.emptyNicknameFieldError = true;
        return;
      } else if (!this.messageContent) {
        this.emptyMessageFieldError = true;
        return;
      }

      message.content = this.messageContent.trimRight();

      const chat: Chat = new Chat();

      chat.messages = [];
      chat.messages.push(message);

      const user: User = new User();
      user.nickname = this.username;
      chat.participants = [];
      chat.participants.push(user);

      this.createChat.emit(chat);

      setTimeout(() => {
        if (!this.userNotFoundError) {
          this.username = '';
        }
      }, 2000);

    } else {

      message.content = this.messageContent.trimRight();
      message.chatId = this.chat.id;
      this.sendMessage.emit(message);

      setTimeout(() => {
        this.updateMessagesScrollBar(); }, 1000);
    }

    this.messageContent = '';
  }
}

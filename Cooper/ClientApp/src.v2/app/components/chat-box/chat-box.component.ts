import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Chat, Message } from '@models';

@Component({
  selector: 'coop-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent {

  public dike: Date = new Date();
  public messageContent: string;

  @Input() public currentSessionUserId: number;
  @Input() public chat: Chat;
  @Output() public sendMessage: EventEmitter<Message> = new EventEmitter<Message>();

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

  public createMessage(messageContent: string): void {
    const message: Message = new Message();
    message.content = messageContent.trimRight();
    message.chatId = this.chat.id;
    message.senderId = this.currentSessionUserId;
    message.isRead = false;
    message.createDate = new Date();

    this.sendMessage.emit(message);

    (document.getElementById('box') as HTMLInputElement).value = '';
  }
}

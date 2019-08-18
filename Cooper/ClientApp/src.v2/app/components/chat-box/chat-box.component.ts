import { Component, Input } from '@angular/core';
import { Chat, Message } from '@models';

@Component({
  selector: 'coop-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent {

  public garbage: string;

  @Input() public currentUserId: number;
  @Input() public chat: Chat;

  public KeyEnter(message: string): void {
    this.garbage = message;
  }

  public getUserAvatar(message: Message): string {
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
}

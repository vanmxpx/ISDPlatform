import { Component, Input, Output, EventEmitter } from '@angular/core';
import {Chat, Message, User} from '@models';

@Component({
  selector: 'coop-chat-modal-window',
  templateUrl: './chat-modal-window.component.html',
  styleUrls: ['./chat-modal-window.component.scss']
})
export class ChatModalWindowComponent {

  @Input() public visibility: boolean;
  @Output() public closeModalWindow: EventEmitter<void> = new EventEmitter<void>();
  @Output() public createChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public onCloseClick(): void {
    this.closeModalWindow.emit();
  }

  public onSendClick(): any { // I rewrite in a normal way creating chat
    const chat: Chat = new Chat();
    chat.messages = [];
    chat.participants = [];
    chat.isOnetoOneChat = true;

    const messageContent = (document.getElementById('message') as HTMLInputElement).value;
    const message: Message = new Message();
    message.content = messageContent;

    chat.messages.push(message);

    const username = (document.getElementById('nickname') as HTMLInputElement).value;
    const user: User = new User();
    user.nickname = username;

    chat.participants.push(user);

    this.createChat.emit(chat);
  }
}

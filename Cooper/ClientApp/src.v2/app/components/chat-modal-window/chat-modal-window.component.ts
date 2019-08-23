import { Component, Input, Output, EventEmitter } from '@angular/core';
import {Chat, Message, User} from '@models';

@Component({
  selector: 'coop-chat-modal-window',
  templateUrl: './chat-modal-window.component.html',
  styleUrls: ['./chat-modal-window.component.scss']
})
export class ChatModalWindowComponent {

  public emptyMessageFieldError: boolean = false;
  public emptyMessageErrorContent: string = 'Please, enter your message first';

  public emptyNicknameFieldError: boolean = false;
  public emptyNicknameErrorContent: string = 'Please, point message recipient before sending a message';

  public notFoundUserError: boolean = false;
  public notFoundUserErrorContent: string = 'User is not found';

  @Input() public visibility: boolean;
  @Output() public closeModalWindow: EventEmitter<void> = new EventEmitter<void>();
  @Output() public createChat: EventEmitter<Chat> = new EventEmitter<Chat>();

  public onCloseClick(): void {
    this.closeModalWindow.emit();
    this.ResetValuesToDefault();
  }

  public onSendClick(): void {

    const messageContent = (document.getElementById('message') as HTMLInputElement).value;
    const username = (document.getElementById('nickname') as HTMLInputElement).value;

    if (!messageContent) {
      this.emptyMessageFieldError = true;
      return;
    } else if (!username) {
      this.emptyNicknameFieldError = true;
      return;
    }

    const chat: Chat = new Chat();
    chat.isOnetoOneChat = true;

    const message: Message = new Message();
    message.content = messageContent;
    chat.messages = [];
    chat.messages.push(message);

    const user: User = new User();
    user.nickname = username;
    chat.participants = [];
    chat.participants.push(user);

    this.createChat.emit(chat);

    setTimeout(() => {if (!this.visibility) {
      this.ResetValuesToDefault();
    } else {
      this.notFoundUserError = true;
    } }, 2000);
  }

  public ResetValuesToDefault(): void {
    this.emptyMessageFieldError = false;
    this.emptyNicknameFieldError = false;
    this.notFoundUserError = false;

    (document.getElementById('message') as HTMLInputElement).value = '';
    (document.getElementById('nickname') as HTMLInputElement).value = '';
  }
}

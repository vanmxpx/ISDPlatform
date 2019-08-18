import { Component } from '@angular/core';
import {ChatService} from '@services';
import {Chat} from '@models';

@Component({
  selector: 'coop-personal-chats',
  templateUrl: './personal-chats.layout.html',
  styleUrls: ['./personal-chats.layout.scss']
})
export class PersonalChatsLayoutComponent {

  public modalWindowVisibility: boolean = false;
  public chatsList: Chat[];
  public currentChat: Chat;

  constructor(private chatService: ChatService) {
    this.getCurrentUserChats();
  }

  public openModalWindow(): void {

    this.modalWindowVisibility = true;

  }

  public closeModalWindow(): void {

    this.modalWindowVisibility = false;

  }

  public async getCurrentUserChats(): Promise<any> {
    this.chatsList = await this.chatService.getPersonalChats();

    if (this.chatsList !== null && this.chatsList.length > 0) {
      this.currentChat = this.chatsList[0];
    }
  }

  public loadChat(chat: Chat): void {
    this.currentChat = chat;
  }
}

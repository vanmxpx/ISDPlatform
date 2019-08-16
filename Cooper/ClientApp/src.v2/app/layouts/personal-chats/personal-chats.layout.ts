import { Component } from '@angular/core';
import {ChatService} from '@services';
import {DummyChat} from '@models';

@Component({
  selector: 'coop-personal-chats',
  templateUrl: './personal-chats.layout.html',
  styleUrls: ['./personal-chats.layout.scss']
})
export class PersonalChatsLayoutComponent {

  public modalWindowVisibility: boolean = false;
  public chatsList: DummyChat[];

  constructor(private chatService: ChatService) {
    this.getCurrentUserChats();
  }

  public openModalWindow(): void {

    this.modalWindowVisibility = true;

  }

  public closeModalWindow(): void {

    this.modalWindowVisibility = false;

  }

  public getCurrentUserChats(): void {
    this.chatsList = this.chatService.getDummyChats();
  }
}

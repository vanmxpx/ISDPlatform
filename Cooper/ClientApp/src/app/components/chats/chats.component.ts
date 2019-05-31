import { Component, OnInit } from '@angular/core';
import { Chat } from '../../models/chat';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css']
})
export class ChatsComponent implements OnInit {

  chats: Chat[];

  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.getChats();
  }

  getChats(): void {
    this.chatService.getChats()
        .subscribe(chats => this.chats = chats);
  }

  add(chatName: string): void {
    chatName = chatName.trim();
    if (!chatName) { return; }
    this.chatService.addChat({ chatName } as Chat)
      .subscribe(chat => {
        this.chats.push(chat);
      });
  }

  delete(chat: Chat): void {
    this.chats = this.chats.filter(h => h !== chat);
    this.chatService.deleteChat(chat).subscribe();
  }

}

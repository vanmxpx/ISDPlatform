import { Component, OnInit, Input } from '@angular/core';
import { Message } from '../../models/message';
import { MessageService } from '../../services/message.service';
//import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
  //providers: [ChatService]
})
export class MessagesComponent implements OnInit {

  @Input() mylistFromParent = [];

  messages: Message[];

  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages(): void {
    this.messageService.getMessages()
        .subscribe(messages => this.messages = messages);
  }

  add(idSender: number, idChat:number, content:string): void {
    //content = content.trim();
    if (!content) { return; }
    this.messageService.addMessage({ idSender, idChat, content } as Message)
      .subscribe(message => {
        this.messages.push(message);
      });
  }

  delete(message: Message): void {
    this.messages = this.messages.filter(h => h !== message);
    this.messageService.deleteMessage(message).subscribe();
  }

}

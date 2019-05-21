import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel } from '@aspnet/signalr';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat';
//import { MessagesComponent } from '../../components/messages/messages.component';
//import { MessageService } from '../../services/message.service';
//import { Message } from '../../models/message';

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.css'],
  //providers: [MessageService]
})

export class ChatDetailComponent implements OnInit {

  private connection: HubConnection;

  constructor(
    private route: ActivatedRoute,
    private chatService: ChatService,
    //private messagesComponent: MessagesComponent,
    //private messageService: MessageService,
    private location: Location,
    private matInputModule: MatInputModule,
    private matListModule: MatListModule,
  ) 
  {
  }

  @Input() chat: Chat;

  ngOnInit() {

    this.getChat();

    this.connection = new HubConnectionBuilder()
      .withUrl("/groupchathub",
        {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets
        })
      .configureLogging(LogLevel.Debug)
      .build();

    console.log(this.connection);

    this.connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    console.log(this.connection.state);

    this.connection.on('Send', (response: any) => {
      console.log(response);
      this.add();
    })

    this.connection.on('SendDM', (message: string, onlineUser: string) => {
      console.log('DM received');
  });
  }

  getChat(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.chatService.getChat(id)
      .subscribe(chat => this.chat = chat);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.chatService.updateChat(this.chat)
      .subscribe(() => this.goBack());
  }

  send(messageContent: string): void {
    messageContent = messageContent.trim();
    if (!messageContent) { return; }
    const idChat = +this.route.snapshot.paramMap.get('id');
    this.connection
      .invoke
      ('Send', idChat, messageContent)
      .then(() => messageContent = '')
      .catch(err => console.error(err));
  }

  sendDirectMessage(message: string, userId: string): string {
     if (this.connection) {
        this.connection.invoke('SendDM', message, userId);
    }
    return message;
}

  mylistFromParent = [];
  
  add() {
    const idChat = +this.route.snapshot.paramMap.get('id');
    this.mylistFromParent.pop();
    this.mylistFromParent.push({"idChat": idChat});
  }

}

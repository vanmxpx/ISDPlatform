import { Component } from '@angular/core';
import {ChatService, SessionService} from '@services';
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel} from '@aspnet/signalr';
import {Chat, Message} from '@models';

@Component({
  selector: 'coop-personal-chats',
  templateUrl: './personal-chats.layout.html',
  styleUrls: ['./personal-chats.layout.scss']
})
export class PersonalChatsLayoutComponent {

  public modalWindowVisibility: boolean = false;
  public chatsList: Chat[];
  public currentChat: Chat;
  public currentUserId: number;

  private hubConnection: HubConnection;

  constructor(private chatService: ChatService, private sessionService: SessionService) {
    this.fetchChatData();
    this.connectWebSockets();
  }

  public openModalWindow(): void {

    this.modalWindowVisibility = true;

  }

  public closeModalWindow(): void {

    this.modalWindowVisibility = false;

  }

  public async fetchChatData(): Promise<any> {
    this.chatsList = await this.chatService.getPersonalChats();

    if (this.chatsList !== null && this.chatsList.length > 0) {
      this.currentChat = this.chatsList[0];
    }

    this.currentUserId = this.sessionService.GetSessionUserId();

    this.loadChat(this.chatsList[0]);
  }

  public connectWebSockets(): void {
    this.hubConnection = new HubConnectionBuilder().withUrl('/chat',
     {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets
    })
    .configureLogging(LogLevel.Debug)
    .build();

    this.hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch((err) => console.log('Error while establishing connection :(' + err));

    this.hubConnection.on('BroadcastMessage', (newMessage: Message) => {
        this.currentChat.messages = this.currentChat.messages.concat(newMessage);
    });
  }

  public loadChat(chat: Chat): void {
    this.currentChat = chat;
  }

  public sendMessage(message: Message): void {
    this.chatService.createMessage(message);
  }
}

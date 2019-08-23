import { Component } from '@angular/core';
import {ChatService, SessionService, UserService} from '@services';
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
  public currentSessionUserId: number;

  private hubConnection: HubConnection;

  constructor(private chatService: ChatService, private sessionService: SessionService, private userService: UserService) {
    this.fetchChatData();
    this.connectWebSockets();
  }

  public async fetchChatData(): Promise<any> {
    this.chatsList = await this.chatService.getPersonalChats();

    if (this.chatsList && this.chatsList.length > 0) {
      this.currentChat = this.chatsList[0];
    }

    this.currentSessionUserId = this.sessionService.GetSessionUserId();

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

    this.hubConnection.on('BroadcastChat', (newChat: Chat) => {
      this.chatsList = this.chatsList.concat(newChat);
  });
  }

  public changeModalWindowVisibility(): void {

    this.modalWindowVisibility = !this.modalWindowVisibility;

  }

  public loadChat(chat: Chat): void {
    this.currentChat = chat;
  }

  public  createChat(chat: Chat): void {
    this.createChatAsync(chat);
  }

  public async createChatAsync(chat: Chat): Promise<any> {
    chat.participants[0] = await this.userService.getUserByNickname(chat.participants[0].nickname);

    if (!chat.participants[0]) {
      return;
    }
    this.modalWindowVisibility = false;

    chat.participants.push(await this.sessionService.getSessionUserData());
    chat.messages[0].senderId = this.currentSessionUserId;

    this.chatService.createChat(chat);
  }

  public sendMessage(message: Message): void {
    this.chatService.createMessage(message);
  }
}

import { Component } from '@angular/core';
import {ChatService, SessionService, UserService} from '@services';
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel} from '@aspnet/signalr';
import {Chat, Message, User} from '@models';

@Component({
  selector: 'coop-personal-chats',
  templateUrl: './personal-chats.layout.html',
  styleUrls: ['./personal-chats.layout.scss']
})
export class PersonalChatsLayoutComponent {

  public userNotFoundError: boolean = true;
  public chatsList: Chat[];
  public currentChat: Chat;
  public currentSessionUserId: number;
  public currentSessionUser: User;
  public newMessageBlockOpened: boolean = false;

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
    this.currentSessionUser = await this.sessionService.getSessionUserData();

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

  public openNewMessageBlock(): void {

    this.newMessageBlockOpened = true;

  }

  public closeNewMessageBlock(): void {

    this.newMessageBlockOpened = false;

  }

  public loadChat(chat: Chat): void {
    this.currentChat = chat;
  }

  public  sendMsgThroughSpecialBlock(chat: Chat): void {
    this.sendMsgThroughSpecialBlockAsync(chat);
  }

  public async sendMsgThroughSpecialBlockAsync(chat: Chat): Promise<any> {

    chat.participants[0] = await this.userService.getUserByNickname(chat.participants[0].nickname);

    if (!chat.participants[0]) {
      this.userNotFoundError = true;
      return;
    }

    const participants: User[] = [chat.participants[0], this.currentSessionUser];

    chat.messages[0].senderId = this.currentSessionUserId;

    this.chatService.sendMessage(chat.messages[chat.messages.length - 1], participants );
  }

  public sendMessage(message: Message): void {
    this.chatService.sendMessage(message, this.currentChat.participants);
  }
}

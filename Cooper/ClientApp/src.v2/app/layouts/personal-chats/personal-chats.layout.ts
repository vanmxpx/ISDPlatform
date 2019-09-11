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

  public userNotFoundError: boolean = false;
  public chatsList: Chat[] = null;
  public currentChat: Chat = null;
  public currentSessionUserId: number;
  public currentSessionUser: User;
  public newMessageBlockOpened: boolean = false;

  private hubConnection: HubConnection;

  constructor(private chatService: ChatService, private sessionService: SessionService, private userService: UserService) {
    this.fetchChatData();
    this.connectWebSockets();
  }

  public async fetchChatData(): Promise<any> {

    try {
      this.currentSessionUser = await this.sessionService.getSessionUserData();

      setTimeout(() => {
      this.currentSessionUserId = this.currentSessionUser.id;
      }, 200);
    } catch (e) {
      console.log('Error: {0}', e);
    }

    try {
      this.chatsList = await this.chatService.getPersonalChats();
    } catch (e) {
      console.log('Error: {0}', e);
    }

    if (this.chatsList && this.chatsList.length > 0) {

      this.loadChat(this.chatsList[0]);
    }

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
        this.setCurrentChat(newMessage.chatId);

        this.currentChat.messages = this.currentChat.messages.concat(newMessage);
    });

    this.hubConnection.on('BroadcastChat', (newChat: Chat) => {

      if (this.currentChat) {
        this.chatsList = this.chatsList.concat(newChat);
      } else {
        this.chatsList = [newChat];
      }

      this.setCurrentChat(newChat.id);
  });
  }

  public setCurrentChat(chatId: number): void {
    this.chatsList.forEach((element) => {
      this.newMessageBlockOpened = false;
      if (element.id === chatId) {
        this.loadChat(element);
      }
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

    try {
      chat.participants[0] = await this.userService.getUserByNickname(chat.participants[0].nickname);
    } catch (e) {
      console.log('Error: {0}', e);
      this.userNotFoundError = true;
      return;
    }

    const participants: User[] = [chat.participants[0], this.currentSessionUser];

    chat.messages[0].senderId = this.currentSessionUser.id;

    this.chatService.sendMessage(chat.messages[chat.messages.length - 1], participants );

  }

  public sendMessage(message: Message): void {
    this.chatService.sendMessage(message, this.currentChat.participants);
  }
}

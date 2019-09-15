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
      setTimeout(() => {
        this.sortChats();
      }, 200);
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
        if (this.isOwnChat(newMessage.chatId)) {

          const chat: Chat = this.getChatById(newMessage.chatId);
          chat.messages = chat.messages.concat(newMessage);

          const isNotCurrentChat: boolean = chat.id !== this.currentChat.id;
          const isCurrentButNotOpenedChat: boolean = chat.id === this.currentChat.id && this.newMessageBlockOpened;

          if ((isNotCurrentChat || isCurrentButNotOpenedChat) && newMessage.senderId !== this.currentSessionUserId) {
            chat.unreadMessagesAmount++;
          }

          this.sortChats();

          if (this.newMessageBlockOpened && newMessage.senderId === this.currentSessionUserId) {
            this.newMessageBlockOpened = false;
          }
        }
    });

    this.hubConnection.on('BroadcastChat', (newChat: Chat) => {
      if (this.isOwnNewChat(newChat)) {

        if (this.currentChat) {
          this.chatsList = this.chatsList.concat(newChat);
        } else {
          this.chatsList = [newChat];
        }

        this.sortChats();

        if (this.chatsList.length === 1) {
          this.currentChat = newChat;
        }

      }
  });

  }

  public loadChat(chat: Chat): void {
    this.newMessageBlockOpened = false;
    this.currentChat = chat;
  }

  public readMessages(chat: Chat): void {
    if (chat.unreadMessagesAmount !== 0) {
      chat.unreadMessagesAmount = 0;

      this.chatService.readNewMessages(chat);
    }
  }

  public openNewMessageBlock(): void {

    this.newMessageBlockOpened = true;

  }

  public closeNewMessageBlock(): void {

    this.newMessageBlockOpened = false;

  }

  public sendMsgThroughSpecialBlock(chat: Chat): void {
    this.sendMsgThroughSpecialBlockAsync(chat);
  }

  public async sendMsgThroughSpecialBlockAsync(chat: Chat): Promise<any> {

    try {
      chat.participants[0] = await this.userService.getUserByNickname(chat.participants[0].nickname);
    } catch (e) {
      console.log(`Error: ${e}`);
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

  private isOwnChat(chatId: number): boolean {

    for (const i in this.chatsList) {
      if (this.chatsList[i].id === chatId) {
        return true;
      }
    }

    return false;
  }

  private isOwnNewChat(chat: Chat): boolean {

    for (const participant of chat.participants) {
      if (participant.id === this.currentSessionUserId) {
        return true;
      }
    }

    return false;
  }

  private sortChats(): void {
    this.chatsList.sort((chat1, chat2): number => {
        const date1: Date = new Date(Date.parse(chat1.messages[chat1.messages.length - 1].createDate.toString()));
        const date2: Date = new Date(Date.parse(chat2.messages[chat2.messages.length - 1].createDate.toString()));

        if (date1 > date2) { return -1; }
        if (date1 < date2) { return 1; }
        return 0;
    });
  }

  private getChatById(chatId: number): Chat {
    for (const chat of this.chatsList) {
      if (chat.id === chatId) {
        return chat;
      }
    }

    return null;
  }
}

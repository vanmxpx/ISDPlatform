import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chat, Message, User} from '@models';
import { Subscription } from 'rxjs';

const chatsUrl = 'api/chats';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  constructor(private httpClient: HttpClient) {}

  public async getPersonalChats(): Promise<Chat[]> {
    const response = await this.httpClient.get<Chat[]>(chatsUrl + '/one-to-one-chats').toPromise();
    return response;
  }

  public sendMessage(message: Message, participants: User[]): Subscription {

    const body = {message, participants};

    return this.httpClient.post(chatsUrl + '/send-message', body).subscribe(() => {
      console.log(`Message with id ${message.id} was succesfully sended.`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }

  public readNewMessages(chat: Chat): void {
    this.httpClient.post(chatsUrl + '/read-messages', chat).subscribe(() => {
      console.log(`Messages from chat with id ${chat.id} were succesfully readed.`);
    },
      (err) => {
        console.log(`Error: ${err}`);
      });
  }
}

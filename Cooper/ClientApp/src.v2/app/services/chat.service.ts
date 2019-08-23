import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chat, Message} from '@models';
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

  public createMessage(message: Message): Subscription {
    return this.httpClient.post(chatsUrl + '/send-message', message).subscribe(() => {
      console.log('Message with id {0} was succesfully created.', message.id);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }

  public createChat(chat: Chat): Subscription {
    return this.httpClient.post(chatsUrl + '/create-chat', chat).subscribe((chatId) => {
      console.log('Chat with id {0} was succesfully created.', chatId);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }
}

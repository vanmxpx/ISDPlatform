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
      console.log('Message with id {0} was succesfully sended.', message.id);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }
}

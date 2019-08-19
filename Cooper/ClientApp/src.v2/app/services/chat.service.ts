import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
    return this.httpClient.post(chatsUrl + '/send-message', message, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(() => {
      console.log('User with id {0} was succesfully updated.', message.id);
    },
      (err) => {
        console.log('Error: {0}', err);
      });
  }
}

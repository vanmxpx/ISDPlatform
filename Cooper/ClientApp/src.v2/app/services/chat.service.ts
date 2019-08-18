import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chat} from '@models';

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
}

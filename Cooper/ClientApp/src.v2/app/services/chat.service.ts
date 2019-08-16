import { Injectable } from '@angular/core';
import {DummyChat} from '@models';
// import { SessionService} from '@services';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private chatsList: DummyChat[] = [

    {
      nickname: 'Andre',
      lastMessage: 'Hi, there',
      photoUrl: 'https://hrcdn.net/s3_pub/hr-avatars/e3ecb178-bfb0-4254-9995-3895d7bb5c5c/150x150.png',
    },
    {
      nickname: 'Mike',
      lastMessage: 'Golden boy',
      photoUrl: 'https://secure.gravatar.com/avatar/7470df8813df7a1a50e3b0365e3dc000?d'
      + '=https://d3rpyts3de3lx8.cloudfront.net/hackerrank/assets/gravatar.jpg&s=150',
    },

  ];
  // constructor(private sessionService: SessionService) {}

  public getDummyChats(): DummyChat[] {
    return this.chatsList;
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonChat } from '../models/my-page-models';
import { ConvertionService } from './convertion.service';
import { CommonChatData } from '../models/data-models/common-chats';

@Injectable({
  providedIn: 'root'
})
export class CommonChatService {

  constructor(private httpClient: HttpClient, private convertionService: ConvertionService) { }

  async fetchCommonChat(): Promise<Array<CommonChat>> { 
    const response = await this.httpClient.get<Array<CommonChatData>>("/common").toPromise();
    return this.convertionService.mapCommonChats(response);
  }

  async newMessage(text: string): Promise<string>{
    const _ = await this.httpClient.post("/common/send", {
      "Text": text
    }).toPromise();
    return "";
  }

}

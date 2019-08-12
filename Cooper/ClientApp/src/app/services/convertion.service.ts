import { Injectable } from '@angular/core';
import { CommonChatData } from '../models/data-models/common-chats';
import { CommonChat } from '../models/my-page-models';
import { TimerService } from './timer.service';

@Injectable({
  providedIn: 'root'
})
export class ConvertionService {

  mapCommonChats(dataMessages: CommonChatData[]): CommonChat[]{
    return dataMessages.map ((element) => this.mapCommonMessage(element))  
  }

  mapCommonMessage(dataMessage: CommonChatData): CommonChat{
    return {
      name: dataMessage.author.nickname,
      avatar: dataMessage.author.photoUrl,
      message: dataMessage.content,
      date: this.timeService.timeAgo(dataMessage.createDate)
    } 
  }

  constructor(private timeService: TimerService) { }
}

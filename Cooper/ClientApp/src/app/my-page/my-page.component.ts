import { Component, OnInit } from '@angular/core';
import { MatCard } from '@angular/material';
import {Game, Comment, CommonChat} from '../models/my-page-models'
import { HttpClient} from '@angular/common/http';

import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel} from '@aspnet/signalr';
import { Message } from 'primeng/api';

@Component({
  selector: 'cooper-my-page',
  templateUrl: './my-page.component.html',
  styleUrls: ['./my-page.component.css']
})
export class MyPageComponent implements OnInit {

  games: Game[] = [
  {
    name: 'tanks',
    logo: 'assets/imageKeeper/WebTanks.png'
  },
  {
    name: 'fortnite',
    logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
  }, 
  {
    name: 'Mafia 2',
    logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
  },
  {
    name: 'tanks',
    logo: 'assets/imageKeeper/WebTanks.png'
  },
  {
    name: 'fortnite',
    logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
  }, 
  {
    name: 'Mafia 2',
    logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
  },
  {
    name: 'tanks',
    logo: 'assets/imageKeeper/WebTanks.png'
  },
  {
    name: 'fortnite',
    logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
  }, 
  {
    name: 'Mafia 2',
    logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
  }
]

comments: Comment[]=
[{
  name: 'User1',
  comment: 'You are cool',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User2',
  comment: 'You  not cool',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User1',
  comment: 'You are robot',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User1',
  comment: 'You are TEST',
  avatar:'assets/imageKeeper/robot.png'
}]

constructor(private httpClient: HttpClient){}

  private hubConnection: HubConnection;
  response : any = {
    nickname: ""
  };
  message = '';
  messages: CommonChat[] = [];
  id: any;
  private _hubConnection: HubConnection;
  nick = '';
  msgs: Message[] = [];
  ngOnInit() {
    this.httpClient.get("/users/nickname/my")
      .subscribe((response)=>{
         this.response = response;
      })  
      this.httpClient.get("/common")
      .subscribe( (response: Array<any>)  =>{
        this.messages =  response.map ((element) => ({
              name: element.author.nickname,
              avatar: '',
              message: element.content,
              date: this.timeAgo(element.createDate)
        }))  
      })
    this.hubConnection = new HubConnectionBuilder().withUrl("/chatCommon", 
     {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets
    })
    .configureLogging(LogLevel.Debug)
    .build();
    console.log(this.hubConnection);
    
    this.hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing connection :(' + err));
    console.log(this.hubConnection.state);
    this.hubConnection.on('BroadcastMessage', (response: any) => {
      console.log(response)
      this.timeAgo(response.createDate)
this.messages = this.messages.concat([{
  name: response.author.nickname,
  avatar: '',
  message: response.content,
  date: this.timeAgo(response.createDate)
}])
  })
}

timeAgo(createDate: number): string {
  var date = Date.now();
  var definition = date-createDate*1000;
  if(definition< 60000) return "recently"
  if(definition< 3600000) return this.formatStringMinutes(Math.round(definition/60000))
  if(definition< 86400000) return this.formatStringHours(Math.round(definition/86400000))
}

formatStringMinutes(number: number): string {
  if(number == 1) return "a minute ago"
  else return number + " minutes ago"
}

formatStringHours(number: number): string {
  if(number == 1) return "an hour ago"
  else return number + " hours ago"
}

sendMessage() {
  if((this.message!=null)&&(this.message!="")) {
  
  this.httpClient.post("/common/send", {
     "Text": this.message
  })
      .subscribe((response)=>{
        this.message = "";
      })  
    }
  }


  exit(): void {
    localStorage.removeItem('JwtCooper');
    window.location.reload();
  }

}

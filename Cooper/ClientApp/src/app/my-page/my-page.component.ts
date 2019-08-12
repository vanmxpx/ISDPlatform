import { Component, OnInit } from '@angular/core';
import { Game, Comment, CommonChat, ProfileList, Profile } from '../models/my-page-models';
import { ListOfGamesService } from '../services/list-of-games.service';
import { UsersCommentsService } from '../services/users-comments.service';
import { HubConnection, HubConnectionBuilder, HttpTransportType, LogLevel} from '@aspnet/signalr';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersFriendsService } from '../services/users-friends.service';
import { CommonChatService } from '../services/common-chat.service';
import { ConvertionService } from '../services/convertion.service';
import { CommonChatData } from '../models/data-models/common-chats';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'cooper-my-page',
  templateUrl: './my-page.component.html',
  styleUrls: ['./my-page.component.css'],
  providers: [ListOfGamesService, UsersCommentsService, UsersFriendsService]
})
export class MyPageComponent implements OnInit {  

  games: Game[] = [];
  comments: Comment[] = [];
  friends: ProfileList[] = [];
  friendsNumber: Number = 0;
  followings: ProfileList[] = [];
  followingsNumber: Number = 0;
  followers: ProfileList[] = [];
  followersNumber: Number = 0;
  messages: CommonChat[] = [];
  profile: Profile = new Profile();
  message: string = "";

  constructor(private route: ActivatedRoute, 
              private router: Router, 
              private commonChatService: CommonChatService,
              private gameService: ListOfGamesService, 
              private usersCommentService: UsersCommentsService, 
              private usersFriendsService: UsersFriendsService,
              private convertionService: ConvertionService,
              private profileService: ProfileService){}

  private hubConnection: HubConnection;

  ngOnInit() { 
    var nick = this.route.snapshot.paramMap.get("nickname"); 
    this.fetchData(nick);
    this.connectSocket();
  }

  goToProfile(nickname: string){
    this.router.navigate(["/myPage", nickname]);
    this.fetchProfile(nickname);
  }

  connectSocket(){
    this.hubConnection = new HubConnectionBuilder().withUrl("/chatCommon", 
     {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets
    })
    .configureLogging(LogLevel.Debug)
    .build();
    
    this.hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing connection :(' + err));
    
    this.hubConnection.on('BroadcastMessage', (newMessage: CommonChatData) => {
        this.messages = this.messages.concat(this.convertionService.mapCommonMessage(newMessage))
    })
  }

  exit(){
    localStorage.removeItem('JwtCooper');
    window.location.reload();
  }

  sendMessage(){
    if( (this.message != null)&&(this.message != "") ) this.messageRequest();
  }

  async messageRequest() {
    const response = await this.commonChatService.newMessage(this.message);
    this.message = response;
  }

  async fetchData(nickname: string){
    this.games = this.gameService.getData();
    this.comments =this.usersCommentService.getData();
    this.friends = await this.usersFriendsService.getFriends();
    this.followers = await this.usersFriendsService.getFollowers();
    this.followings = await this.usersFriendsService.getFollowings();
    this.friendsNumber = this.friends.length;
    this.followersNumber = this.followers.length;
    this.followingsNumber = this.followings.length;
    this.messages = await this.commonChatService.fetchCommonChat();
    this.fetchProfile(nickname); 
  }

  async fetchProfile(nickname: string) {
    this.profile = await this.profileService.getUsersProfile(nickname);
  }

}
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService, GamesService, UsersInteractionService } from '@services';
import { User, Game} from '@models';

@Component({
  selector: 'coop-profile-layout',
  templateUrl: './profile.layout.html',
  styleUrls: ['./profile.layout.css']
})

export class ProfileLayoutComponent implements OnInit {

  games: Game[] = [];
  friends: User[] = [];
  subscriptions: User[] = [];
  subscribers: User[] = [];
  friendsAmount = 0;
  subscriptionsAmount = 0;
  subscribersAmount = 0;
  profile: User;
  sessionProfile: User;
  isSessionProfile = false;

  constructor(private route: ActivatedRoute, private router: Router,
              private gameDummyService: GamesService,
              private usersInteractionService: UsersInteractionService,
              private userService: UserService) {}

    ngOnInit() {
      this.updateProfile();
    }

    updateProfile() {
      const nickname = this.route.snapshot.paramMap.get('nickname');
      this.fetchProfileData(nickname);
      this.fetchSessionProfileData();

      this.getAllGames();
    }

    exit() {
      localStorage.removeItem('JwtCooper');
      window.location.reload();
    }

    async fetchProfileData(nickname: string) {
      this.profile = await this.userService.getUserByNickname(nickname);


      this.friends = await this.usersInteractionService.getFriends(this.profile.id);
      if (this.friends) {
        this.friendsAmount = this.friends.length;
      } else {
        this.friendsAmount = 0;
      }

      this.subscribers = await this.usersInteractionService.getSubscribers(this.profile.id);
      if (this.subscribers) {
        this.subscribersAmount = this.subscribers.length;
      } else {
        this.subscribersAmount = 0;
      }

      this.subscriptions = await this.usersInteractionService.getSubscriptions(this.profile.id);
      if (this.subscriptions) {
        this.subscriptionsAmount = this.subscriptions.length;
      } else {
        this.subscriptionsAmount = 0;
      }
    }

    async fetchSessionProfileData() {
      this.sessionProfile = await this.userService.getUserByToken();
    }

    // Dummy method
    private getAllGames() {
      this.games = this.gameDummyService.mockedGames;
    }

    async goToProfile(nickname: string) {
      await this.router.navigate(['/platform/profile', nickname]);
      this.updateProfile();
    }

    /*
    goToProfile(nickname: string) {
      this.router.navigate(['/profile', nickname]);
      this.fetchProfile(nickname);
    }

    async fetchData(nickname: string) {
      this.games = this.gameService.getData();
      this.comments = this.usersCommentService.getData();
      this.friends = await this.usersFriendsService.getFriends();
      this.subscribers = await this.usersFriendsService.getFollowers();
      this.subscriptions = await this.usersFriendsService.getFollowings();
      this.friendsNumber = this.friends.length;
      this.followersNumber = this.subscribers.length;
      this.followingsNumber = this.subscriptions.length;
      this.fetchProfile(nickname);
    }

    */
}

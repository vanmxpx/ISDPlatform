import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService, GamesService, UsersInteractionService, SessionService } from '@services';
import { User, Game} from '@models';

@Component({
  selector: 'coop-profile-layout',
  templateUrl: './profile.layout.html',
  styleUrls: ['./profile.layout.css']
})

export class ProfileLayoutComponent implements OnInit {

  public games: Game[] = [];
  public friends: User[] = [];
  public subscriptions: User[] = [];
  public subscribers: User[] = [];

  public friendsAmount: number = 0;
  public subscriptionsAmount: number = 0;
  public subscribersAmount: number = 0;

  public profile: User;
  public isOwnProfile: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router,
              private gameDummyService: GamesService,
              private usersInteractionService: UsersInteractionService,
              private userService: UserService,
              private sessionService: SessionService) {
              }

    public ngOnInit(): void {
      this.updateProfile();
    }

    public updateProfile(): void {
      const nickname = this.route.snapshot.paramMap.get('nickname');
      this.fetchProfileData(nickname);

      this.getAllGames();
    }

    public async fetchProfileData(nickname: string): Promise<any> {
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

      this.isOwnProfile = this.sessionService.GetSessionUserId() === this.profile.id;
    }

    // Dummy method
    private getAllGames(): void {
      this.games = this.gameDummyService.mockedGames;
    }

    public async goToProfile(nickname: string): Promise<any> {
      await this.router.navigate(['/platform/profile', nickname]);
      this.updateProfile();
    }

    public updateSessionUserInfo(updatedUser: User): void {
      if (this.isOwnProfile) {
        this.userService.updateUserInfo(updatedUser);
      }
    }
}

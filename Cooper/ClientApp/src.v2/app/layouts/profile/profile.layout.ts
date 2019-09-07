import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService, GamesService, UsersSocialConnectionsService, SessionService } from '@services';

import { User, Game} from '@models';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { UploadLayoutComponent } from '../upload-form/upload-form.layout';

@Component({
  selector: 'coop-profile-layout',
  templateUrl: './profile.layout.html',
  styleUrls: ['./profile.layout.css']
})

export class ProfileLayoutComponent implements OnInit, OnDestroy {

  public games: Game[] = [];
  public friends: User[] = [];
  public subscriptions: User[] = [];
  public subscribers: User[] = [];

  public friendsAmount: number = 0;
  public subscriptionsAmount: number = 0;
  public subscribersAmount: number = 0;

  public profile: User;
  public isOwnProfile: boolean = false;

  public routeChangeSubscription: Subscription;

  constructor(private route: ActivatedRoute, private router: Router,
              private gameDummyService: GamesService,
              private usersSocialConnectionsService: UsersSocialConnectionsService,
              private userService: UserService,
              private sessionService: SessionService,
              public dialog: MatDialog) {}

  public ngOnInit(): void {
      this.routeChangeSubscription = this.route.queryParams.subscribe(() => {
        this.route.params.subscribe(() => {
            this.updateProfile();
        });
      });
    }

  public ngOnDestroy(): void {
      this.routeChangeSubscription.unsubscribe();
  }

  public openDialog(): void {
    if (this.sessionService.GetSessionUserId() === this.profile.id) {
      const dialogRef = this.dialog.open(UploadLayoutComponent, {
        data: { type: 'avatar' }
      });
      dialogRef.afterClosed().subscribe(
        (url) => {
          if (url != null) {
            this.profile.photoURL = url;
            this.updateProfile();
          }
        });
    }
  }

  public updateProfile(): void {
    const nickname = this.route.snapshot.paramMap.get('nickname');
    this.fetchProfileData(nickname);

    this.getAllGames();
  }

  public async fetchProfileData(nickname: string): Promise<any> {
    this.profile = await this.userService.getUserByNickname(nickname);

    this.friends = await this.usersSocialConnectionsService.getFriends(this.profile.id);
    if (this.friends) {
      this.friendsAmount = this.friends.length;
    } else {
      this.friendsAmount = 0;
    }

    this.subscribers = await this.usersSocialConnectionsService.getSubscribers(this.profile.id);
    if (this.subscribers) {
      this.subscribersAmount = this.subscribers.length;
    } else {
      this.subscribersAmount = 0;
    }

    this.subscriptions = await this.usersSocialConnectionsService.getSubscriptions(this.profile.id);
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

  public async updateAvatar(url: string): Promise<any> {
    console.log(url);
    this.profile.photoURL = url;
    this.updateProfile();
  }

  public updateSessionUserInfo(updatedUser: User): void {
    if (this.isOwnProfile) {
      this.userService.updateUserInfo(updatedUser);
    }
  }
}

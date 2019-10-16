import { Component, OnInit } from '@angular/core';
import { SessionService } from '@services';
import { User } from '@models';
import { SocialNetwork } from '@enums';
import { MatDialog } from '@angular/material/dialog';
import { ChangeEmailComponent, DeleteAccountComponent } from '@components';
import { Router } from '@angular/router';
import { SettingsService } from '@services';

@Component({
  selector: 'coop-settings-layout',
  templateUrl: './settings.layout.html',
  styleUrls: ['./settings.layout.css']
})

export class SettingsLayoutComponent implements OnInit {
  public googleStatus: string;
  public facebookStatus: string;
  public profile: User;

  constructor(private settingsService: SettingsService, private sessionService: SessionService,
              private router: Router, public dialog: MatDialog) {}

  public ngOnInit(): void {
    this.settingsService.updateAccountEvent.subscribe(() => this.updateProfile());
    this.updateProfile();
  }

  public updateProfile(): void {
    console.log('updating profile...');
    this.sessionService.getSessionUserData().then((user) => {
      this.profile = user;
      this.googleStatus = this.socialCheck(this.profile.googleId);
      this.facebookStatus = this.socialCheck(this.profile.facebookId);
    });
  }

  private socialCheck(value: string): string {
    return (!value) ? 'Not connected' : 'Connected';
  }

  public resetPassword(): void {
    if (this.sessionService.GetSessionUserId() === this.profile.id) {
      this.router.navigate(['/reset']);
    }
  }

  public deleteAccount(): void {
    if (this.sessionService.GetSessionUserId() === this.profile.id) {
      const dialogRef = this.dialog.open(DeleteAccountComponent);
      dialogRef.afterClosed().subscribe(
        (result) => {
          if (result) {
            this.settingsService.deleteAccount();
          }
        });
    }
  }

  public changeEmail(): void {
    if (this.sessionService.GetSessionUserId() === this.profile.id) {
      const dialogRef = this.dialog.open(ChangeEmailComponent);
      dialogRef.afterClosed().subscribe(
        (newEmail) => {
          if (newEmail) {
            this.settingsService.changeEmail(newEmail);
          }
        });
    }
  }

  public linkSocialAccount(platform: SocialNetwork): void {
    if (this.sessionService.GetSessionUserId() === this.profile.id) {
      this.settingsService.socialConnect(platform);
    }
  }
}

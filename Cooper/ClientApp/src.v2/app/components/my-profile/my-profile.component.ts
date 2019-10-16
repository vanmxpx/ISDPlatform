import { Component, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import { User } from '@models';

@Component({
  selector: 'coop-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent {

  public editMode: boolean = false;

  @Input() public profile: User;

  @Output() public userUpdated: EventEmitter<User> = new EventEmitter<User>();

  constructor(private router: Router) {}

  public onEditButtonClicked(): void {
    this.editMode = true;
  }

  public onSaveButtonClicked(): void {
    const userUpdated = this.profile;

    userUpdated.name = (document.getElementById('name') as HTMLInputElement).value;
    userUpdated.email = (document.getElementById('email') as HTMLInputElement).value;
    userUpdated.description = (document.getElementById('description') as HTMLInputElement).value;

    this.userUpdated.emit(userUpdated);

    this.editMode = false;
  }

  public settingsRedirect(): void {
    this.router.navigate(['/platform/settings']);
  }
}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from '@models';

@Component({
  selector: 'coop-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  editMode = false;

  constructor() { }

  @Input() profile: User;

  @Output() userUpdated = new EventEmitter<User>();

  ngOnInit() {
  }

  onEditButtonClicked(): void {
    this.editMode = true;
  }

  onSaveButtonClicked(): void {
    const userUpdated = this.profile;


    userUpdated.name = (document.getElementById('name') as HTMLInputElement).value;
    userUpdated.email = (document.getElementById('email') as HTMLInputElement).value;
    userUpdated.description = (document.getElementById('description') as HTMLInputElement).value;

    this.userUpdated.emit(userUpdated);

    this.editMode = false;
  }

}

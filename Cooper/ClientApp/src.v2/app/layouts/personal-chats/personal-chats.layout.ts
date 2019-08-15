import { Component } from '@angular/core';

@Component({
  selector: 'coop-personal-chats',
  templateUrl: './personal-chats.layout.html',
  styleUrls: ['./personal-chats.layout.scss']
})
export class PersonalChatsLayoutComponent {

  public modalWindowVisibility: boolean = false;

  public openModalWindow(): void {

    this.modalWindowVisibility = true;

 }

 public closeModalWindow(): void {

  this.modalWindowVisibility = false;

}

}

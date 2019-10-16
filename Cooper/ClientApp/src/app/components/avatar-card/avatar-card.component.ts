import { Component, Input } from '@angular/core';

@Component({
  selector: 'coop-avatar-card',
  templateUrl: './avatar-card.component.html',
  styleUrls: ['./avatar-card.component.scss']
})
export class AvatarCardComponent {

  @Input() public nickname: string;
  @Input() public photoURL: string;

}

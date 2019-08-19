import { Component, Input, Output, EventEmitter } from '@angular/core';
import {NgForm} from '@angular/forms';
import {Chat} from '@models';

@Component({
  selector: 'coop-chat-modal-window',
  templateUrl: './chat-modal-window.component.html',
  styleUrls: ['./chat-modal-window.component.scss']
})
export class ChatModalWindowComponent {

  @Input() public visibility: boolean;
  @Output() public closeModalWindow: EventEmitter<void> = new EventEmitter<void>();
  @Output() public sendMessage: EventEmitter<NgForm> = new EventEmitter<NgForm>();

  public onCloseClick(): void {
    this.closeModalWindow.emit();
  }
}

import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'coop-message-panel',
  templateUrl: './message-panel.component.html',
  styleUrls: ['./message-panel.component.scss']
})
export class MessagePanelComponent {

  @Output() public openModalWindow: EventEmitter<void> = new EventEmitter<void>();

  public onNewMessageButtonClicked(): void {
    this.openModalWindow.emit();
  }
}

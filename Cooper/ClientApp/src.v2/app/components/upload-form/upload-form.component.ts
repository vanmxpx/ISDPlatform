import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'coop-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.scss']
})
export class UploadComponent {
  @Input() public errorMessage: string;
  @Output() public uploadEvent: EventEmitter<any> = new EventEmitter();

  public dragUploadFile(event: any): void {
    event.preventDefault();
    this.uploadEvent.emit(event.dataTransfer.files[0]);
  }

  public clickUploadFile(event: any): void {
    event.preventDefault();
    this.uploadEvent.emit(event.target.files[0]);
  }

  public dragOver(event: any): void {
    event.stopPropagation();
    event.preventDefault();
  }
}

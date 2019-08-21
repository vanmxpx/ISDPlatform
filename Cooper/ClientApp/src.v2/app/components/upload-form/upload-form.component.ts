import { Component, Inject } from '@angular/core';
import { MediaserverService } from '@services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'coop-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.scss']
})
export class UploadComponent {
  public error: string;

  constructor(private dialogRef: MatDialogRef<UploadComponent>,
              @Inject(MAT_DIALOG_DATA) private data: string,
              private mediaserver: MediaserverService) { }

  public dragUploadFile(event: any): void {
    event.preventDefault();
    this.uploadFile(event.dataTransfer.files[0]);
  }

  public clickUploadFile(event: any): void {
    event.preventDefault();
    this.uploadFile(event.target.files[0]);
  }

  private uploadFile(file: any): void {
    if (!file.type.match('image*/')) {
      this.error = 'File is not an image';
      return;
    }
    /* tslint:disable:no-string-literal */
    if (this.data['type'] != null) {
      this.mediaserver.uploadImage(file, this.data['type']).subscribe((data) => {
        this.dialogRef.close(data);
      },
      (err) => { this.error = err; });
    }
    /* tslint:enable:no-string-literal */
  }

  public dragOver(event: any): void {
    event.stopPropagation();
    event.preventDefault();
  }
}

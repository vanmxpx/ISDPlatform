import { Component, Inject } from '@angular/core';
import { MediaserverService } from '@services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  templateUrl: './upload-form.layout.html',
  styleUrls: ['./upload-form.layout.scss']
})

export class UploadLayoutComponent {
  public error: string;

  constructor(private dialogRef: MatDialogRef<UploadLayoutComponent>,
              @Inject(MAT_DIALOG_DATA) private data: string,
              private mediaserver: MediaserverService) { }

  public uploadFile(file: any): void {
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
}

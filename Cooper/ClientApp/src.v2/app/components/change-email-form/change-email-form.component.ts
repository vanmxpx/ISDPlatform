import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'coop-change-email-form',
  templateUrl: './change-email-form.component.html',
  styleUrls: ['./change-email-form.component.scss']
})
export class ChangeEmailComponent {
  public email: string;

  constructor(private dialogRef: MatDialogRef<ChangeEmailComponent>) {}

  public emailChange(): void {
    this.dialogRef.close(this.email);
  }
}

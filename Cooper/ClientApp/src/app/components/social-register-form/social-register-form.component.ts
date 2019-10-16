import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'coop-social-register-form',
  templateUrl: './social-register-form.component.html',
  styleUrls: ['./social-register-form.component.scss']
})
export class SocialRegisterComponent {
  public nickname: string;

  constructor(private dialogRef: MatDialogRef<SocialRegisterComponent>) {}

  public socialRegister(): void {
    this.dialogRef.close(this.nickname);
  }
}

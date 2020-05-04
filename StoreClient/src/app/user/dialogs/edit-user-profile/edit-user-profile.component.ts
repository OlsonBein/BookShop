import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { UserModelItem } from 'src/app/shared/models';
import { UserService } from 'src/app/shared/services';

@Component({
  selector: 'app-edit-user-profile',
  templateUrl: './edit-user-profile.component.html',
  styleUrls: ['./edit-user-profile.component.css']
})
export class EditUserProfileComponent {
  public userModel: UserModelItem;

  constructor(
    private userService: UserService,
    public dialogRef: MatDialogRef<EditUserProfileComponent>,
    @Inject(MAT_DIALOG_DATA) public user: UserModelItem) {
      this.userModel = new UserModelItem();
    }

  clickCancel(): void {
    this.dialogRef.close();
  }

  saveChanges(): void {
    this.userService.editUserProfile(this.user).subscribe();
  }
}

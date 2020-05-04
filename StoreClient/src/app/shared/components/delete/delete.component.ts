import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent {

  constructor(
    public dialogRef: MatDialogRef<DeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any ) {
    }

  clickCancel(): void {
    this.dialogRef.close();
  }

}

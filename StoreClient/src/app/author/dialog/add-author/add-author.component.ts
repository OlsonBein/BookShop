import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { AuthorService } from 'src/app/shared/services/author.service';
import { AuthorDialogModel } from 'src/app/shared/models/author/author-dialog-model.models';
import { Action } from 'src/app/shared/enums';

@Component({
  selector: 'app-add-author',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.css']
})
export class AddAuthorComponent {

  constructor(
    private authorService: AuthorService,
    private dialogRef: MatDialogRef<AddAuthorComponent>,
    @Inject(MAT_DIALOG_DATA) public authorModel: AuthorDialogModel
    ) { }

  clickCancel(): void {
    this.dialogRef.close();
  }

  chooseMethod(): void {
    if (this.authorModel.actionName === Action.Create) {
      this.createAuthor();
      return;
    }
    this.updateAuthor();
  }

  getActionName(): string {
    return Action[this.authorModel.actionName];
  }

  createAuthor(): void {
    this.authorService.createAuthor(this.authorModel.author).subscribe();
  }

  updateAuthor(): void {
    this.authorService.updateAuthor(this.authorModel.author).subscribe();
  }
}

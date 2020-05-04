import { AuthorModelItem } from 'src/app/shared/models';
import { Action } from 'src/app/shared/enums';

export class AuthorDialogModel {
  public author: AuthorModelItem;
  public actionName: Action;
}

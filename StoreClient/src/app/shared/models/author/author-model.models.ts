import { AuthorModelItem } from 'src/app/shared/models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class AuthorModel extends BaseModel {
  public items: Array<AuthorModelItem>;
  public totalCount: number;
}

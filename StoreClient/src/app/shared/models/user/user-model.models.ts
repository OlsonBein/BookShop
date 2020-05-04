import { UserModelItem } from 'src/app/shared/models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class UserModel extends BaseModel {
  items: Array<UserModelItem>;
  totalCount: number;
}

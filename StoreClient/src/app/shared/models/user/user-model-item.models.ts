import { UserStatus } from 'src/app/shared/enums';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class UserModelItem extends BaseModel {
  public firstName: string;
  public lastName: string;
  public email: string;
  public status: UserStatus;
  public id?: number;
  public oldPassword?: string;
  public newPassword?: string;
  public role?: string;
  public avatar?: string;


}

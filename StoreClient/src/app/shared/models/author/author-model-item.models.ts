import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class AuthorModelItem extends BaseModel {
  public name: string;
  public printingEditionTitles?: Array<string>;
  public id?: number;
}

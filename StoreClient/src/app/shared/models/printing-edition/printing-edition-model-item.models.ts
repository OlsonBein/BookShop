import { ProductType, Currency, OrderStatus } from 'src/app/shared/enums';
import { AuthorModelItem } from 'src/app/shared/models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class PrintingEditionModelItem extends BaseModel {
  public currency: Currency;
  public productType: ProductType;
  public authors?: Array<AuthorModelItem>;
  public price?: number;
  public title?: string;
  public id?: number;
  public description?: string;
  public status?: OrderStatus;
  public sale: number = 0;
}

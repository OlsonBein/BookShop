import { OrderModelItem } from 'src/app/shared/models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class OrderModel extends BaseModel {

  public items: Array<OrderModelItem>;
  public totalCount: number;
}

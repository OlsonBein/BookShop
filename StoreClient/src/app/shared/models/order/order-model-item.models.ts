import { OrderStatus } from 'src/app/shared/enums/entity/order-status.enum';
import { OrderItemModelItem } from 'src/app/shared/models/order-item/order-item-model-item.models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class OrderModelItem extends BaseModel {
  public id: number;
  public paymentDate: string;
  public userName: string;
  public userId: number;
  public userEmail: string;
  public orderItems: Array<OrderItemModelItem>;
  public orderStatus: OrderStatus;
  public count: number;
  public amount: number;
  public paymentId: number;
  public description: string;
}

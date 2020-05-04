import { OrderItemModelItem } from 'src/app/shared/models';

export class OrderItemModel {
  public items: Array<OrderItemModelItem>;
  public totalCount: number;
}

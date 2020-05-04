import { OrderItemModelItem } from 'src/app/shared/models';

export class CartModelItem {
  public orderItems: Array<OrderItemModelItem>;
  public description: string;
  public transactionId: string;
  public totalAmount: number;
}

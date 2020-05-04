import { ProductType } from 'src/app/shared/enums';

export class OrderItemModelItem {
  public printingEditionId?: number;
  public count: number;
  public amount: number;
  public orderId: number;
  public productType: ProductType;
  public productTitle: string;
}

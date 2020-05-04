import { BaseFilterModel } from 'src/app/shared/models/base/base-filter-model.models';
import { OrderStatus } from 'src/app/shared/enums';

export class FilterOrderModel extends BaseFilterModel {
  public orderStatus: OrderStatus;
}

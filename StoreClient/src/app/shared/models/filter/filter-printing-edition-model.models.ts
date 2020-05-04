import { ProductType, Currency, ProductFilteredColumnType } from 'src/app/shared/enums';
import { BaseFilterModel } from 'src/app/shared/models/base/base-filter-model.models';

export class FilterPrintingEditionModel extends BaseFilterModel {
  public productTypes: Array<ProductType>;
  public filteredColumnType: ProductFilteredColumnType;
  public minPrice?: number;
  public maxPrice?: number;
  public currency?: Currency;
}

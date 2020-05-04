import { AuthorFilteredColumnType } from 'src/app/shared/enums';
import { BaseFilterModel } from 'src/app/shared/models/base/base-filter-model.models';

export class FilterAuthorModel extends BaseFilterModel {
  public filteredColumnType?: AuthorFilteredColumnType;
}

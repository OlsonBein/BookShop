import { UserStatus, UserFilteredColumnType } from 'src/app/shared/enums';
import { BaseFilterModel } from 'src/app/shared/models/base/base-filter-model.models';

export class FilterUserModel extends BaseFilterModel {
  public status?: UserStatus;
  public filteredColumnType?: UserFilteredColumnType;
}

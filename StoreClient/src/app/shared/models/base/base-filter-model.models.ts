import { AscendingDescendingSort } from 'src/app/shared/enums';

export class BaseFilterModel {
  public pageCount: number;
  public pageSize: number;
  public sortType?: AscendingDescendingSort;
  public searchString?: string;
}

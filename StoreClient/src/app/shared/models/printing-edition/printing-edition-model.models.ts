import { PrintingEditionModelItem } from 'src/app/shared/models';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class PrintingEditionModel extends BaseModel {
  public items: Array<PrintingEditionModelItem>;
  public totalCount: number;
}

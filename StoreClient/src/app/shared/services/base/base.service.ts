import { Injectable } from '@angular/core';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';
import { EntityConstants } from 'src/app/shared/constants';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor() { }

  handleErrors(errors: string[]) {
    errors.forEach(element => {
      alert(element);
    });
  }

  checkServerErrors(data: BaseModel): BaseModel {
    if (data.errors.length === 0) {
      return;
    }
    data.errors.forEach(element => {
      alert(element);
    });
  }

  getParams(): {withCredentials: boolean} {
    return { withCredentials: EntityConstants.withCredentials };
  }
}

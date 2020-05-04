import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { FilterPrintingEditionModel, PrintingEditionModelItem, PrintingEditionModel, BaseModel } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from './base/base.service';

@Injectable({
  providedIn: 'root'
})
export class PrintingEditionService extends BaseService {
  apiUrl: string;

  constructor(
    private http: HttpClient
  ) {
    super();
    this.apiUrl = environment.apiUrl;
   }

  getAllPrintingEditions(model: FilterPrintingEditionModel): Observable<PrintingEditionModel> {
    return this.http.post<PrintingEditionModel>(`${this.apiUrl}/printingEdition/getAll`, model, this.getParams()).pipe(
        tap((data: PrintingEditionModel) => {
          if (data.errors.length !== 0) {
            return  this.checkServerErrors(data);
          }
          return data;
        })
      );
  }

  createPrintingEdition(model: PrintingEditionModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/printingEdition/create`, model, this.getParams()).pipe(
        tap((data: BaseModel) => {
          return this.checkServerErrors(data);
        })
      );
  }

  getPrintingEditionById(id: number): Observable<PrintingEditionModelItem> {
    return this.http.get<PrintingEditionModelItem>(`${this.apiUrl}/printingEdition/getById?id=${id}`, this.getParams()).pipe(
        tap((data: PrintingEditionModelItem) => {
          if (data.errors.length !== 0) {
            return  this.checkServerErrors(data);
          }
          return data;
        })
      );
  }

  updatePrintingEdition(model: PrintingEditionModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/printingEdition/update`, model, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  deletePrintingEdition(id: number): Observable<BaseModel> {
    return this.http.get<BaseModel>(`${this.apiUrl}/printingEdition/delete?id=${id}`, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { FilterOrderModel, OrderModel } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from './base/base.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends BaseService {
  apiUrl: string;

  constructor(
    private http: HttpClient
  ) {
    super();
    this.apiUrl = environment.apiUrl;
   }

  getAllOrders(model: FilterOrderModel): Observable<OrderModel> {
    return this.http.post<OrderModel>(`${this.apiUrl}/order/getAll`, model, this.getParams()).pipe(
      tap((data: OrderModel) => {
        if (data.errors.length !== 0) {
          return  this.checkServerErrors(data);
        }
        return data;
      })
    );
  }

  getUserOrders(model: FilterOrderModel, id: number): Observable<OrderModel> {
    return this.http.post<OrderModel>(`${this.apiUrl}/order/getByUserId?id=${id}`, model, this.getParams()).pipe(
      tap((data: OrderModel) => {
        if (data.errors.length !== 0) {
          return  this.checkServerErrors(data);
        }
        return data;
      })
    );
  }
}

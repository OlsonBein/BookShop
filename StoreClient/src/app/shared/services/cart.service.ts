import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { CartModelItem, OrderModelItem } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from './base/base.service';

@Injectable({
  providedIn: 'root'
})
export class CartService extends BaseService {
  apiUrl: string;

constructor(
  private http: HttpClient,
) {
  super();
  this.apiUrl = environment.apiUrl;
}

  createOrder(model: CartModelItem): Observable<OrderModelItem> {
    return this.http.post<OrderModelItem>(`${this.apiUrl}/order/create`, model, this.getParams()).pipe(
      tap((data: OrderModelItem) => {
        if (data.errors.length !== 0) {
          return this.checkServerErrors(data);
        }
        return data;
      })
    );
  }
}

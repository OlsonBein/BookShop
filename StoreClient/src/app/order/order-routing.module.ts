import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OrdersComponent } from 'src/app/order/orders/orders.component';
import { UserOrdersComponent } from 'src/app/order/user-orders/user-orders.component';

export const routes: Routes = [
  {path: 'orders', component: OrdersComponent},
  {path: 'userOrders', component: UserOrdersComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from 'src/app/material/material.module';
import { OrderRoutingModule } from 'src/app/order/order-routing.module';

import { OrdersComponent } from 'src/app/order/orders/orders.component';
import { UserOrdersComponent } from 'src/app/order/user-orders/user-orders.component';

import { routes } from 'src/app/order/order-routing.module';

@NgModule({
  declarations: [
    OrdersComponent,
    UserOrdersComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule.forChild(routes)
  ]
})
export class OrderModule { }

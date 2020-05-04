import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CartRoutingModule } from 'src/app/cart/cart-routing.module';

import { PaymentSuccessComponent } from 'src/app/cart/payment-success/payment-success.component';
import { ConfirmPaymentComponent } from 'src/app/cart/confirm-payment/confirm-payment.component';

import { routes } from 'src/app/cart/cart-routing.module';

@NgModule({
  declarations: [
    PaymentSuccessComponent,
    ConfirmPaymentComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
    CartRoutingModule,
    RouterModule.forChild(routes)
  ]
})
export class CartModule { }

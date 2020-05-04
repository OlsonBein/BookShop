import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PaymentSuccessComponent } from 'src/app/cart/payment-success/payment-success.component';
import { ConfirmPaymentComponent } from 'src/app/cart/confirm-payment/confirm-payment.component';

export const routes: Routes = [
  {path: 'paymentSuccess', component: PaymentSuccessComponent},
  {path: 'confirmPayment', component: ConfirmPaymentComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartRoutingModule { }

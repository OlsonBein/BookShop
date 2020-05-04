import { Injectable, Predicate } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  handler: any;

constructor() {
  this.handler = null;
  this.loadStripe();
}

pay(amount: number, callback: Predicate<string>): void {
  let handler = (window as any).StripeCheckout.configure({
    key: 'pk_test_2biwxxXMwRyQ2prliCpzKT6a00ROHgjGOt',
    locale: 'auto',
    token: ((data) => {
      callback(data.id);
    })
  });
  handler.open({
    name: 'Book Store',
    description: '2 widgets',
    amount: amount * 100
  });
}

loadStripe(): void {
  if (!window.document.getElementById('stripe-script')) {
    let stripe = window.document.createElement('script');
    stripe.id = 'stripe-script';
    stripe.type = 'text/javascript';
    stripe.src = 'https://checkout.stripe.com/checkout.js';
    stripe.onload = () => {
      this.handler = (window as any).StripeCheckout.configure({
        key: 'pk_test_2biwxxXMwRyQ2prliCpzKT6a00ROHgjGOt',
        locale: 'auto',
      });
    };
    window.document.body.appendChild(stripe);
    }
  }
}

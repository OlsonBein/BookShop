import { Component, OnInit, Predicate } from '@angular/core';
import { MatDialogRef, MatDialog, MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

import { CartModelItem, OrderItemModelItem, OrderModelItem } from 'src/app/shared/models';
import { LocalStorageHelper, PaymentService, CartService } from 'src/app/shared/services';
import { EntityConstants } from 'src/app/shared/constants';

import { environment } from 'src/environments/environment';
import { PaymentSuccessComponent } from 'src/app/cart/payment-success/payment-success.component';

@Component({
  selector: 'app-confirm-payment',
  templateUrl: './confirm-payment.component.html',
  styleUrls: ['./confirm-payment.component.css']
})
export class ConfirmPaymentComponent implements OnInit {
  cartModel: CartModelItem;
  tableColumns: string[];
  handler: any = null;
  imagePath: string;
  paymentCallBack: Predicate<string>;

  constructor(
    private paymentService: PaymentService,
    private cartService: CartService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog,
    private localStorageHelper: LocalStorageHelper,
    public dialogRef: MatDialogRef<ConfirmPaymentComponent>,
    ) {
      this.cartModel = new CartModelItem();
      this.tableColumns = EntityConstants.cartTableColumns;
      this.imagePath = environment.imagesPath;
    }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('delete', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/delete.svg`));
    this.localStorageHelper.cart.subscribe(data => {
      this.cartModel = data;
    });
    this.paymentCallBack = this.makeOrder.bind(this);
  }

  setUnitPrice(orderItemModel: OrderItemModelItem): string {
    let unitPrice = orderItemModel.amount / orderItemModel.count;
    return `$ ${unitPrice}`;
  }

  setTotalAmount(): number {
    let totalAmount = 0;
    this.cartModel.orderItems.forEach(item => {
      totalAmount += item.amount;
    });
    this.cartModel.totalAmount = totalAmount;
    return totalAmount;
  }

  clickCancel(): void {
    this.dialogRef.close();
  }

  deleteOrderItem(product: OrderItemModelItem): void {
    let result = this.localStorageHelper.removeItemFromCart(product);
    if (result) {
      this.clickCancel();
    }
  }

  makeOrder(transactionId: string): void {
    this.cartModel.transactionId = transactionId;
    this.cartService.createOrder(this.cartModel).subscribe((data: OrderModelItem) => {
      this.openPaymentSuccessDialog(data.id);
    });
    this.clickCancel();
  }

  buyProducts(): void {
    this.paymentService.pay(this.setTotalAmount(), this.paymentCallBack);
  }

  openPaymentSuccessDialog(id: number): void {
    const dialogRef = this.dialog.open(PaymentSuccessComponent, {
      width: '400px',
      data: id
    });
    dialogRef.afterClosed().subscribe(() => {
        this.localStorageHelper.removeCart();
    });
  }
}

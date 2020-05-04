import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { FormControl } from '@angular/forms';

import { PrintingEditionModelItem, OrderItemModelItem } from 'src/app/shared/models';
import { PrintingEditionService, LocalStorageHelper } from 'src/app/shared/services';
import { Currency } from 'src/app/shared/enums';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-printing-edition-details',
  templateUrl: './printing-edition-details.component.html',
  styleUrls: ['./printing-edition-details.component.css']
})
export class PrintingEditionDetailsComponent implements OnInit {
  productModel: PrintingEditionModelItem;
  imagesPath: string;
  id: number;
  subscription: Subscription;
  orderItemModel: OrderItemModelItem;
  authors: FormControl;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private pribtingEditionService: PrintingEditionService,
    private router: Router,
    private activateRoute: ActivatedRoute
  ) {
    this.productModel = new PrintingEditionModelItem();
    this.orderItemModel = new OrderItemModelItem();
    this.subscription = this.activateRoute.params.subscribe(data => {
      this.id = data.id;
    });

    this.authors = new FormControl();
    this.imagesPath = environment.imagesPath;
   }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('mainBook', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/mainBook.svg`));
    this.getPrintingEditionById();
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  getPrintingEditionById(): void {
    this.pribtingEditionService.getPrintingEditionById(this.id).subscribe(data => {
      this.productModel = data;
      this.authors = new FormControl(this.getAuthorsNames());
    });
  }

  getPrice(): string {
    let currencyString;
    currencyString = Currency[this.productModel.currency];
    return `Price: ${this.productModel.price} ${currencyString}`;
  }

  getAuthorsNames(): string[] {
    let authorsNames = [];
    this.productModel.authors.forEach(x => {
      authorsNames.push(x.name);
    });
    return authorsNames;
  }

  addProductToCard(): void {
    this.orderItemModel.printingEditionId = this.productModel.id;
    this.orderItemModel.productTitle = this.productModel.title;
    this.orderItemModel.amount = this.productModel.price * this.orderItemModel.count;
    let sameProduct = this.checkForSameProductInCart();
    if (sameProduct && this.orderItemModel.count > 0) {
      this.localStorageHelper.storeCartItem(this.orderItemModel);
    }
    this.router.navigate(['/printingEdition/userHomePage']);
  }

  private checkForSameProductInCart(): boolean {
    let items = this.localStorageHelper.getCartItems();
    let result = true;
    if (items !== null) {
      items.orderItems.forEach(element => {
        if (element.printingEditionId === this.orderItemModel.printingEditionId) {
          result = false;
        }
      });
    }
    return result;
  }
}

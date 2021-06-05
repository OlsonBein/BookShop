import { Component, OnInit } from '@angular/core';
import { MatIconRegistry, MatDialog } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { AccountService, LocalStorageHelper } from 'src/app/shared/services';
import { ConfirmPaymentComponent } from 'src/app/cart/confirm-payment/confirm-payment.component';
import { UserModelItem, CartModelItem } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  currentUser: UserModelItem;
  currentCart: CartModelItem;
  imagesPath: string;
  image: any;

  constructor(
    private dialog: MatDialog,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    public router: Router,
    private accountService: AccountService,
    private localStorageHelper: LocalStorageHelper
  ) {
    this.currentUser = new UserModelItem();
    this.currentCart = new CartModelItem();
    this.imagesPath = environment.imagesPath;
    this.image = null;
  }

  ngOnInit(): void {
    this.localStorageHelper.user.subscribe((data: UserModelItem) => {
      this.currentUser = data;
      if (this.isLoggedIn()) {
        this.image = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.currentUser.avatar}`);
      }
    });
    this.localStorageHelper.cart.subscribe((data: CartModelItem) => {
      this.currentCart = data;
    });
    this.iconRegistry.addSvgIcon('book', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/bread.svg`))
    .addSvgIcon('cart', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/cart.svg`))
    .addSvgIcon('avatar', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/avatar.svg`));
  }

  isRoleAdmin(): boolean {
    if (this.isLoggedIn()) {
      return this.localStorageHelper.isRoleAdmin();
    }
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  logOut(): void {
    this.localStorageHelper.removeUser();
    this.localStorageHelper.removeCart();
    this.accountService.logOut().subscribe();
    this.router.navigate(['account/logIn']);
  }

  openCartDialog(): void {
    let items = this.localStorageHelper.getCartItems();
    if (items === null) {
      return;
    }
    const dialogRef = this.dialog.open(ConfirmPaymentComponent, {
      width: '800px',
      data: {}
    });
    dialogRef.afterClosed().subscribe();
  }
}

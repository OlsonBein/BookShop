import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';

import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';

import { UserModelItem, TokensModel, CartModelItem, OrderItemModelItem } from 'src/app/shared/models';
import { RoleConstants } from 'src/app/shared/constants';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageHelper {
  public static readonly userItem = 'user';
  public static readonly cartItem = 'cartItem';
  public readonly accessToken = 'AccessToken';
  public readonly refreshToken = 'refreshtoken';
  orderItemModel: Array<OrderItemModelItem>;
  private currentUser = new BehaviorSubject<UserModelItem>(this.getUser());
  user = this.currentUser.asObservable();
  private currentCart = new BehaviorSubject<CartModelItem>(this.getCartItems());
  cart = this.currentCart.asObservable();

  constructor(
    private router: Router,
    private cookieService: CookieService,
  ) {
    this.orderItemModel = new Array<OrderItemModelItem>();
  }

  getTokens(): TokensModel {
    const access = this.cookieService.get(this.accessToken);
    const refresh = this.cookieService.get(this.refreshToken);
    const model = new TokensModel(access, refresh);
    return model;
  }

  saveUser(user: UserModelItem): void {
    localStorage.setItem(LocalStorageHelper.userItem, JSON.stringify(user));
    this.currentUser.next(user);
  }

  getUser(): UserModelItem {
    let user = JSON.parse(localStorage.getItem(LocalStorageHelper.userItem));
    return user;
  }

  removeUser(): void {
    localStorage.removeItem(LocalStorageHelper.userItem);
  }

  isLoggedIn(): boolean {
    return localStorage.getItem(LocalStorageHelper.userItem) ? true : false;
  }

  getRole(): string {
    let tokens = this.getTokens();
    let dekodedToken = jwt_decode(tokens.accessToken);
    let role: string = dekodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role;
  }

  isRoleAdmin(): boolean {
    let role = RoleConstants.admin;
    return this.currentUser.value.role === role ? true : false;
  }

  private storeFirstItem(cartModelItem: OrderItemModelItem ): void {
    let cartModel = new CartModelItem();
    this.orderItemModel.push(cartModelItem);
    cartModel.orderItems = this.orderItemModel;
    localStorage.setItem(LocalStorageHelper.cartItem, JSON.stringify(cartModel));
    this.currentCart.next(cartModel);
  }

  storeCartItem(cartModelItem: OrderItemModelItem ): void {
    let cartModel = this.getCartItems();
    if (cartModel === null) {
      this.storeFirstItem(cartModelItem);
    }
    if (cartModel !== null) {
      this.orderItemModel = this.getCartItems().orderItems;
      this.orderItemModel.push(cartModelItem);
      cartModel.orderItems = this.orderItemModel;
      localStorage.setItem(LocalStorageHelper.cartItem, JSON.stringify(cartModel));
      this.currentCart.next(cartModel);
    }
  }

  getCartItems(): CartModelItem {
    return JSON.parse(localStorage.getItem(LocalStorageHelper.cartItem));
  }

  removeCart(): void {
    localStorage.removeItem(LocalStorageHelper.cartItem);
    this.orderItemModel = [];
  }

  removeItemFromCart(product: OrderItemModelItem): boolean {
    let cartModel = this.getCartItems();
    for (let i = 0; i < cartModel.orderItems.length; i++ ) {
      if (cartModel.orderItems[i].printingEditionId === product.printingEditionId) {
        cartModel.orderItems.splice(i, 1);
      }
    }
    localStorage.setItem(LocalStorageHelper.cartItem, JSON.stringify(cartModel));
    let emptyCartResult = this.isCartEmpty(cartModel);
    this.currentCart.next(cartModel);
    return emptyCartResult;
  }

  private isCartEmpty(cartModel: CartModelItem): boolean {
    if (cartModel.orderItems.length === 0) {
      this.currentCart.next(null);
      this.orderItemModel = [];
      this.removeCart();
      return true;
    }
    return false;
  }
}

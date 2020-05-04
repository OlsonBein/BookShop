import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: 'account', loadChildren: () => import('src/app/account/account.module').then( x => x.AccountModule)},
  {path: 'user', loadChildren: () => import('src/app/user/user.module').then(x => x.UserModule)},
  {path: 'author', loadChildren: () => import('src/app/author/author.module').then(x => x.AuthorModule)},
  {path: 'printingEdition', loadChildren: () => import('src/app/printing-edition/printing-edition.module')
    .then(x => x.PrintingEditionModule)},
  {path: 'order', loadChildren: () => import('src/app/order/order.module').then(x => x.OrderModule)},
  {path: 'cart', loadChildren: () => import('src/app/cart/cart.module').then(x => x.CartModule)},
  { path: '',   redirectTo: '/printingEdition/userHomePage', pathMatch: 'full' }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    onSameUrlNavigation: 'reload'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

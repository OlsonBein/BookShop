import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PrintingEditionsComponent } from 'src/app/printing-edition/printing-editions/printing-editions.component';
import { AddPrintingEditionComponent } from 'src/app/printing-edition/dialog/add-printing-edition/add-printing-edition.component';
import { PrintingEditionDetailsComponent } from 'src/app/printing-edition/printing-edition-details/printing-edition-details.component';
import { HomeComponent } from 'src/app/printing-edition/user-home-page/user-home-page.component';

export const routes: Routes = [
  {path: 'PrintingEditions', component: PrintingEditionsComponent},
  {path: 'addPrintingEdition', component: AddPrintingEditionComponent},
  {path: 'userHomePage', component: HomeComponent},
  {path: 'productDetails/:id', component: PrintingEditionDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrintingEditionRoutingModule { }

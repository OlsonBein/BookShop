import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';

import { PrintingEditionRoutingModule } from 'src/app/printing-edition/printing-edition-routing.module';

import { PrintingEditionsComponent } from 'src/app/printing-edition/printing-editions/printing-editions.component';
import { AddPrintingEditionComponent } from 'src/app/printing-edition/dialog/add-printing-edition/add-printing-edition.component';
import { PrintingEditionDetailsComponent } from 'src/app/printing-edition/printing-edition-details/printing-edition-details.component';
import { HomeComponent } from 'src/app/printing-edition/user-home-page/user-home-page.component';

import { routes } from 'src/app/printing-edition/printing-edition-routing.module';



@NgModule({
  declarations: [
    PrintingEditionsComponent,
    AddPrintingEditionComponent,
    PrintingEditionDetailsComponent,
    HomeComponent
  ],
  imports: [
    SharedModule,
    CommonModule,
    PrintingEditionRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class PrintingEditionModule { }

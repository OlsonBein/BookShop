import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material/material.module';

import { SharedRoutingModule } from './shared-routing.module';
import {CartModule } from 'src/app/cart/cart.module';
import { DeleteComponent } from 'src/app/shared/components/delete/delete.component';

@NgModule({
  declarations: [
    DeleteComponent
  ],
  imports: [
    CartModule,
    CommonModule,
    MaterialModule,
    SharedRoutingModule,
  ],
  providers: []
})
export class SharedModule { }

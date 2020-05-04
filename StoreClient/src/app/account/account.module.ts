import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material/material.module';

import { RegistrationComponent } from 'src/app/account/registration/registration.component';
import { ConfirmEmailComponent } from 'src/app/account/confirm-email/confirm-email.component';
import { ResetPasswordComponent } from 'src/app/account/reset-password/reset-password.component';
import { LogInComponent } from 'src/app/account/log-in/log-in.component';

import {routes} from 'src/app/account/account-routing.module';

@NgModule({
  declarations: [
  RegistrationComponent,
  ConfirmEmailComponent,
  ResetPasswordComponent,
  LogInComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule.forChild(routes)
  ]
})
export class AccountModule { }

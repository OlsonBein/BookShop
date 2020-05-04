import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material/material.module';
import { UserRoutingModule } from 'src/app/user/user-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {SharedModule } from 'src/app/shared/shared.module';

import { GetAllUsersComponent } from 'src/app/user/users/users.component';
import { EditUserProfileComponent } from 'src/app/user/dialogs/edit-user-profile/edit-user-profile.component';
import { ProfileComponent } from 'src/app/user/profile/profile.component';

import { routes } from 'src/app/user/user-routing.module';


@NgModule({
  declarations: [
    GetAllUsersComponent,
    EditUserProfileComponent,
    ProfileComponent
    ],
  imports: [
    SharedModule,
    CommonModule,
    UserRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class UserModule { }

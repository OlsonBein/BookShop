import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GetAllUsersComponent } from 'src/app/user/users/users.component';
import { EditUserProfileComponent } from 'src/app/user/dialogs/edit-user-profile/edit-user-profile.component';
import { ProfileComponent } from 'src/app/user/profile/profile.component';

export const routes: Routes = [
  {path: 'users', component: GetAllUsersComponent},
  {path: 'editUserProfile', component: EditUserProfileComponent},
  {path: 'profile', component: ProfileComponent}
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }

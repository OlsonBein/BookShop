import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegistrationComponent } from 'src/app/account/registration/registration.component';
import { ConfirmEmailComponent } from 'src/app/account/confirm-email/confirm-email.component';
import { ResetPasswordComponent } from 'src/app/account/reset-password/reset-password.component';
import { LogInComponent } from 'src/app/account/log-in/log-in.component';

export const routes: Routes = [
    {path: 'confirmEmail', component: ConfirmEmailComponent },
    {path: 'logIn', component: LogInComponent},
    {path: 'registration', component: RegistrationComponent},
    {path: 'resetPassword', component: ResetPasswordComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }

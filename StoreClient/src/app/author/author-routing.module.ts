import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorsComponent } from 'src/app/author/authors/authors.component';
import { AddAuthorComponent } from 'src/app/author/dialog/add-author/add-author.component';

export const routes: Routes = [
  {path: 'authors', component: AuthorsComponent},
  {path: 'addAuthor', component: AddAuthorComponent}
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }

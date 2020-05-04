import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

import { AuthorsComponent } from 'src/app/author/authors/authors.component';
import { AddAuthorComponent } from 'src/app/author/dialog/add-author/add-author.component';

import { routes } from 'src/app/author/author-routing.module';

@NgModule({
  declarations: [
    AuthorsComponent,
    AddAuthorComponent
    ],
  imports: [
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class AuthorModule { }

import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';

const MaterialComponents = [
    MatButtonModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatInputModule,
    MatTableModule,
    MatDialogModule,
    MatIconModule,
    MatMenuModule,
    MatAutocompleteModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatPaginatorModule,
    MatSortModule,
    MatGridListModule,
    MatExpansionModule,
    MatSidenavModule,
    MatTooltipModule
];

@NgModule({
  imports: [
    MaterialComponents
  ],
  exports: [
    MaterialComponents
    ]
})
export class MaterialModule { }

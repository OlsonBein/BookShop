import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialog, MatIconRegistry, PageEvent } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

import { AuthorService, LocalStorageHelper } from 'src/app/shared/services';
import { AuthorModel, AuthorModelItem } from 'src/app/shared/models';
import { AscendingDescendingSort, AuthorFilteredColumnType, Action, NameEntityToDelete } from 'src/app/shared/enums';
import { EntityConstants, FilterConstants } from 'src/app/shared/constants';
import { environment } from 'src/environments/environment';
import { AddAuthorComponent } from 'src/app/author/dialog/add-author/add-author.component';
import { DeleteComponent } from 'src/app/shared/components/delete/delete.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css'],
  providers: [AuthorService]
})
export class AuthorsComponent implements OnInit {
  authors: AuthorModel;
  filterForm: FormGroup;
  totalAuthorsCount: number;
  tableColumns: string[];
  productTableColumns: string[];
  pageSizeOptions: number[];
  pageSize: number;
  imagesPath: string;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private router: Router,
    private authorService: AuthorService,
    private dialog: MatDialog,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private formBuilder: FormBuilder
    ) {
      this.imagesPath = environment.imagesPath;
      this.authors = new AuthorModel();
      this.tableColumns = EntityConstants.authorTableColumns;
      this.pageSizeOptions = EntityConstants.pageSizeOptions;

      this.filterForm = this.formBuilder.group({
        searchString: [FilterConstants.emptyLine, Validators.minLength(3)],
        filteredColumnType: [AuthorFilteredColumnType.id],
        sortType: [AscendingDescendingSort.asc],
        pageSize: [FilterConstants.pageSize],
        pageCount: [FilterConstants.pageCount]
      });
     }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon(
      'pencil', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/pencil.svg`)).addSvgIcon(
      'delete', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/delete.svg`)).addSvgIcon(
      'search', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/search.svg`)).addSvgIcon(
      'add', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/add.svg`));
    if (!this.localStorageHelper.isRoleAdmin()) {
      this.router.navigate(['/printingEdition/home']);
    }
    this.getFiltered();
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);
    this.getFiltered();
  }

  checkSearchString(): void {
    if (this.filterForm.controls.searchString.value.length >= 3 ||
      this.filterForm.controls.searchString.value.length === 0) {
      this.getFiltered();
    }
  }

  getFiltered(): void {
    this.authorService.getAllAuthors(this.filterForm.value).subscribe(
      (data: AuthorModel) => {
        this.totalAuthorsCount = data.totalCount;
        this.authors = data;
    });
  }

  setAscendingSort(direction: string, column: string): void {
    this.filterForm.controls.sortType.setValue(AscendingDescendingSort[direction]);
    this.filterForm.controls.filteredColumnType.setValue(AuthorFilteredColumnType[column]);

    this.getFiltered();
  }

  private openDialog(author: AuthorModelItem, actionName: Action): void {
    const dialogRef = this.dialog.open(AddAuthorComponent, {
      width: '400px',
      data: {author, actionName}
    });
    dialogRef.afterClosed().subscribe(() => {
        this.getFiltered();
    });
  }

  openCreateDialog(): void {
    let author = new AuthorModelItem();
    let actionName = Action.Create;
    this.openDialog(author, actionName);
  }

  openUpdateDialog(author: AuthorModelItem): void {
    let actionName = Action.Update;
    this.openDialog(author, actionName);
  }

  openDeleteDialog(id: number): void {
    let nameEntityToDelete = "Food distributor";
    const dialogRef = this.dialog.open(DeleteComponent, {
      width: '400px',
      data: {nameEntityToDelete}
    });
    dialogRef.afterClosed().subscribe(data => {
      if (data === true) {
        this.authorService.deleteAuthor(id).subscribe(() => {
          this.getFiltered();
        });
      }
    });
  }
}

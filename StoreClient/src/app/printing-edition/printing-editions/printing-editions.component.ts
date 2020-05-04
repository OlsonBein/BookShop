import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { MatDialog, MatIconRegistry, PageEvent } from '@angular/material';

import { PrintingEditionModelItem, PrintingEditionModel, FilterPrintingEditionModel, AuthorModel, UserModelItem } from 'src/app/shared/models';
import { FilterConstants, EntityConstants } from 'src/app/shared/constants';
import { AscendingDescendingSort, ProductType, ProductFilteredColumnType, Action, NameEntityToDelete } from 'src/app/shared/enums';

import { PrintingEditionService, LocalStorageHelper } from 'src/app/shared/services';
import { AddPrintingEditionComponent } from 'src/app/printing-edition/dialog/add-printing-edition/add-printing-edition.component';
import { DeleteComponent } from 'src/app/shared/components/delete/delete.component';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-printing-editions',
  templateUrl: './printing-editions.component.html',
  styleUrls: ['./printing-editions.component.css'],
  providers: [PrintingEditionService]
})
export class PrintingEditionsComponent implements OnInit {
  currentUser: UserModelItem;
  printingEditions: PrintingEditionModel;
  filterModel: FilterPrintingEditionModel;
  authors: AuthorModel;
  filterForm: FormGroup;
  productType: FormControl;
  tableColumns: string[];
  pageSizeOptions: number[];
  totalProductsCount: number;
  productTypes: string[];
  imagePath: string;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private printingEditionService: PrintingEditionService,
    private dialog: MatDialog,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private router: Router,
    private formBuilder: FormBuilder
    ) {
      this.currentUser = new UserModelItem();
      this.imagePath = environment.imagesPath;
      this.printingEditions = new PrintingEditionModel();
      this.filterModel = new FilterPrintingEditionModel();
      this.authors = new AuthorModel();

      this.tableColumns = EntityConstants.printingEditionTableColumns;
      this.pageSizeOptions = EntityConstants.pageSizeOptions;
      this.productTypes = EntityConstants.productTypes;

      this.filterForm = this.formBuilder.group({
        searchString: [FilterConstants.emptyLine, Validators.minLength(3)],
        filteredColumnType: [ProductFilteredColumnType.id],
        sortType: [AscendingDescendingSort.asc],
        pageSize: [FilterConstants.pageSize],
        pageCount: [FilterConstants.pageCount]
      });
      this.productType = new FormControl(this.productTypes);
    }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon(
      'pencil', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/pencil.svg`)).addSvgIcon(
      'delete', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/delete.svg`)).addSvgIcon(
      'search', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/search.svg`)).addSvgIcon(
      'add', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/add.svg`));
    this.localStorageHelper.user.subscribe(data => {
      this.currentUser = data;
    });
    this.getFiltered();
  }

  isRoleAdmin(): boolean {
    return this.localStorageHelper.isRoleAdmin();
  }

  getFiltered(): void {
    this.filterModel = this.filterForm.value;
    this.setProductType(this.productType.value);
    this.printingEditionService.getAllPrintingEditions(this.filterModel).subscribe(data => {
      this.totalProductsCount = data.totalCount;
      this.printingEditions = data;
    });
  }

  checkSearchString(): void {
    if (this.filterForm.controls.searchString.value.length >= 3 ||
      this.filterForm.controls.searchString.value.length === 0) {
      this.getFiltered();
    }
  }

  getProductType(type: number): string {
    return ProductType[type];
  }

  private setProductType(productTypes: string[]): void {
    const types = new Array<ProductType>();

    productTypes.forEach(element => {
      types.push(ProductType[element]);
    });
    this.filterModel.productTypes = types;
  }

  setAscendingSort(direction: string, column: string): void {
    this.filterForm.controls.sortType.setValue(AscendingDescendingSort[direction]);
    this.filterForm.controls.filteredColumnType.setValue(ProductFilteredColumnType[column]);

    this.getFiltered();
  }

  private openDialog(printingEdition: PrintingEditionModelItem, actionName: Action): void {
    const dialogRef = this.dialog.open(AddPrintingEditionComponent, {
      width: '800px',
      data: {printingEdition, actionName}
    });
    dialogRef.afterClosed().subscribe(() => {
      this.getFiltered();
    });
  }

  openCreateDialog(): void {
    let printingEdition = new PrintingEditionModelItem();
    let actionName = Action.Create;
    this.openDialog(printingEdition, actionName);
  }

  openUpdateDialog(printingEdition: PrintingEditionModelItem): void {
    let actionName = Action.Update;
    this.openDialog(printingEdition, actionName);
  }

  openDeleteDialog(id: number): void {
    let nameEntityToDelete = NameEntityToDelete[NameEntityToDelete.PrintingEdition];
    const dialogRef = this.dialog.open(DeleteComponent, {
      width: '400px',
      data: {nameEntityToDelete}
    });
    dialogRef.afterClosed().subscribe((data: boolean) => {
      if (data === true) {
        this.printingEditionService.deletePrintingEdition(id).subscribe(() => {
          this.getFiltered();
        });
      }
    });
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);

    this.getFiltered();
  }
}

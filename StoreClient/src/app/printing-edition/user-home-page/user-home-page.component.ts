import { Component, OnInit } from '@angular/core';
import { MatIconRegistry, PageEvent } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { PrintingEditionModel, PrintingEditionModelItem, AuthorModelItem, FilterPrintingEditionModel, UserModelItem } from 'src/app/shared/models';
import { FilterConstants, EntityConstants } from 'src/app/shared/constants';
import { Currency, ProductType, ProductFilteredColumnType, AscendingDescendingSort } from 'src/app/shared/enums';
import { PrintingEditionService, LocalStorageHelper } from 'src/app/shared/services';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-home-page',
  templateUrl: './user-home-page.component.html',
  styleUrls: ['./user-home-page.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: UserModelItem;
  printingEditions: PrintingEditionModel;
  filterModel: FilterPrintingEditionModel;
  totalProductCount: number;
  filterForm: FormGroup;
  types: string[];
  productTypes: FormControl;
  booksOnPage: PrintingEditionModelItem[];
  currency: string[];
  filterPriceDirection: string[];
  pageSizeOptions: number[];
  minPrice: number;
  maxPrice: number;
  imagesPath: string;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private printingEditionService: PrintingEditionService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.currentUser = new UserModelItem();
    this.imagesPath = environment.imagesPath;
    this.filterModel = new FilterPrintingEditionModel();
    this.printingEditions = new PrintingEditionModel();

    this.types = EntityConstants.productTypes;
    this.currency = EntityConstants.currency;
    this.filterPriceDirection = FilterConstants.priceDirection;
    this.pageSizeOptions = EntityConstants.mainPageSizeOptions;
    this.booksOnPage = [];
    this.minPrice = FilterConstants.minPrice;
    this.maxPrice = FilterConstants.maxPrice;

    this.filterForm = this.formBuilder.group({
      searchString: [FilterConstants.emptyLine, Validators.minLength(3)],
      filteredColumnType: [ProductFilteredColumnType.price],
      formCurrency: [Currency.USD],
      sortType: [AscendingDescendingSort.asc],
      pageSize: [FilterConstants.mainPageSize],
      pageCount: [FilterConstants.pageCount]
    });
    this.productTypes = new FormControl(this.types);
   }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('mainBook', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/mainBook.svg`))
      .addSvgIcon('search', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/search.svg`))
      .addSvgIcon('settings', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/settings.svg`));
    this.localStorageHelper.user.subscribe(data => {
      this.currentUser = data;
    });
    this.getFiltered();
  }

  isRoleAdmin(): boolean {
    return this.localStorageHelper.isRoleAdmin();
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  getFiltered(): void {
    this.filterModel = this.filterForm.value;
    this.setProductType(this.productTypes.value);
    this.filterModel.minPrice = this.minPrice;
    this.filterModel.maxPrice = this.maxPrice;
    this.filterModel.currency = this.filterForm.value.formCurrency;
    this.printingEditionService.getAllPrintingEditions(this.filterModel).subscribe(data => {
      this.printingEditions = data;
      this.totalProductCount = data.totalCount;
      this.setBookOnPage();
    });
  }

  checkMinLength(): void {
    if (this.filterForm.controls.searchString.value.length >= 3 ||
      this.filterForm.controls.searchString.value.length === 0) {
      this.getFiltered();
    }
  }

  setBookOnPage(): void {
    this.booksOnPage.splice(0);
    this.printingEditions.items.forEach(element => {
      this.booksOnPage.push(element);
    });
  }

  displayAuthors(authors: Array<AuthorModelItem>): string[] {
    const authorNames = [];
    authors.forEach(element => {
      authorNames.push(element.name);
    });
    return authorNames;
  }

  setProductType(productTypes: string[]): void {
    const types: Array<ProductType> = new Array<ProductType>();
    productTypes.forEach(element => {
    types.push(ProductType[element]);
    });
    this.filterModel.productTypes = types;
  }

  setCurrency(element: string): void {
    this.filterForm.value.formCurrency = Currency[element];

    this.getFiltered();
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);

    this.getFiltered();
  }

  setDirectionSort(direction: string): void {
    this.filterForm.value.sortType = AscendingDescendingSort[direction];

    this.getFiltered();
  }

  getPrice(price: number, currencyType: string): string {
    return `Price: ${price} ${Currency[currencyType]}`;
  }

  getProductDetails(productId: number): void {
    this.router.navigate([`/printingEdition/productDetails/${productId}`]);
  }
}

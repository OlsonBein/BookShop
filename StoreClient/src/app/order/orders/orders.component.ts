import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material';

import { AscendingDescendingSort, ProductType, OrderStatus, OrderFilteredColumnType } from 'src/app/shared/enums';
import { OrderModel, FilterOrderModel } from 'src/app/shared/models';
import { OrderService, LocalStorageHelper } from 'src/app/shared/services';
import { EntityConstants, FilterConstants } from 'src/app/shared/constants';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
  providers: [OrderService]
})
export class OrdersComponent implements OnInit {
  orders: OrderModel;
  filterModel: FilterOrderModel;
  tableColumns: string[];
  pageSizeOptions: number[];
  totalOrdersCount: number;
  filterForm: FormGroup;
  orderStatus: string[];
  currentStatus: FormControl;
  imagesPath: string;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private router: Router,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
    ) {
      this.imagesPath = environment.imagesPath;
      this.orders = new OrderModel();
      this.filterModel = new FilterOrderModel();
      this.tableColumns = EntityConstants.orderTableColumns;
      this.pageSizeOptions = EntityConstants.pageSizeOptions;
      this.orderStatus = EntityConstants.orderStatus;

      this.filterForm = this.formBuilder.group({
        searchString: [FilterConstants.emptyLine, Validators.minLength(3)],
        filteredColumnType: [OrderFilteredColumnType.id],
        sortType: [AscendingDescendingSort.asc],
        pageSize: [FilterConstants.pageSize],
        pageCount: [FilterConstants.pageCount]
      });
      this.currentStatus = new FormControl(this.orderStatus);
      }

  ngOnInit(): void {
    this.iconRegistry
        .addSvgIcon('search', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/search.svg`));
    if (!this.localStorageHelper.isRoleAdmin()) {
      this.router.navigate(['/printingEdition/userHomePage']);
    }
    this.getFiltered();
  }

  getProductType(type: number): string {
    return ProductType[type];
  }

  getStatusType(type: number): string {
    return OrderStatus[type];
  }

  getFiltered(): void {
    this.filterModel = this.filterForm.value;
    this.orderService.getAllOrders(this.filterModel).subscribe(data => {
      this.orders = data;
      this.totalOrdersCount = data.totalCount;
    });
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);

    this.getFiltered();
  }

  setAscendingSort(direction: string, column: string): void {
    this.filterForm.controls.sortType.setValue(AscendingDescendingSort[direction]);
    this.filterForm.controls.filteredColumnType.setValue(OrderFilteredColumnType[column]);

    this.getFiltered();
  }

  checkSearchString(): void {
    if (this.filterForm.controls.searchString.value.length >= 3 ||
      this.filterForm.controls.searchString.value.length === 0) {
      this.getFiltered();
    }
  }
}

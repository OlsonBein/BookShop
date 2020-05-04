import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { PageEvent } from '@angular/material';

import { ProductType, OrderStatus, OrderFilteredColumnType, AscendingDescendingSort } from 'src/app/shared/enums';
import { OrderService, LocalStorageHelper } from 'src/app/shared/services';
import { OrderModel, FilterOrderModel } from 'src/app/shared/models';
import { EntityConstants, FilterConstants } from 'src/app/shared/constants';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.css']
})
export class UserOrdersComponent implements OnInit {
  orders: OrderModel;
  userId: number;
  filterForm: FormGroup;
  tableColumns: string[];
  pageSizeOptions: number[];
  totalOrdersCount: number;
  orderStatus: string[];
  filterModel: FilterOrderModel;

  constructor(
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private localStorageHelper: LocalStorageHelper,
  ) {
    this.orders = new OrderModel();
    this.tableColumns = EntityConstants.userOrderTableColumn;
    this.pageSizeOptions = EntityConstants.pageSizeOptions;
    this.orderStatus = EntityConstants.orderStatus;
    this.filterModel = new FilterOrderModel();

    this.filterForm = this.formBuilder.group({
      searchString: [FilterConstants.emptyLine],
      filteredColumnType: [OrderFilteredColumnType.id],
      sortType: [AscendingDescendingSort.asc],
      pageSize: [FilterConstants.pageSize],
      pageCount: [FilterConstants.pageCount]
      });
   }

  ngOnInit(): void {
    this.getUserId();
    this.getUserOrders();
  }

  getUserId(): void {
    this.userId = this.localStorageHelper.getUser().id;
  }

  getProductType(type: number): string {
    return ProductType[type];
  }

  getStatusType(type: number): string {
    return OrderStatus[type];
  }

  getUserOrders(): void {
    this.orderService.getUserOrders(this.filterForm.value, this.userId).subscribe( data => {
      this.orders = data;
      this.totalOrdersCount = data.totalCount;
    });
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);

    this.getUserOrders();
  }

  setAscendingSort(direction: string, column: string): void {
    this.filterForm.controls.sortType.setValue(AscendingDescendingSort[direction]);
    this.filterForm.controls.filteredColumnType.setValue(OrderFilteredColumnType[column]);

    this.getUserOrders();
  }
}

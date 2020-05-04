import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material';
import { FormGroup } from '@angular/forms';
import { EntityConstants } from 'src/app/shared/constants';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  pageSizeOptions: number[];
  pageSize: number;

  constructor() {
    this.pageSizeOptions = EntityConstants.pageSizeOptions;
   }


  setPaginationOptions($event: PageEvent, filterForm: FormGroup): FormGroup {
    filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    filterForm.controls.pageSize.setValue($event.pageSize);
    return filterForm;
  }

}

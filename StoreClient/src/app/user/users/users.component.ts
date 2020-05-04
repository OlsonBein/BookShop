import { Component, OnInit } from '@angular/core';
import { MatIconRegistry, PageEvent } from '@angular/material';
import { MatDialog } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';

import { EntityConstants, FilterConstants } from 'src/app/shared/constants';
import {FilterUserModel, UserModel } from 'src/app/shared/models';
import {UserStatus, AscendingDescendingSort, UserFilteredColumnType, NameEntityToDelete} from 'src/app/shared/enums';
import { UserService, LocalStorageHelper } from 'src/app/shared/services';
import { EditUserProfileComponent } from 'src/app/user/dialogs/edit-user-profile/edit-user-profile.component';
import { DeleteComponent } from 'src/app/shared/components/delete/delete.component';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-get-all-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [UserService]
})
export class GetAllUsersComponent implements OnInit {
  users: UserModel;
  filterForm: FormGroup;
  filterModel: FilterUserModel;
  tableColumns: string[];
  statuses: string[];
  status: FormControl;
  totalUsersCount: number;
  pageSizeOptions: number[];
  imagePath: string;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private userService: UserService,
    private dialog: MatDialog,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private router: Router,
    private formBuilder: FormBuilder
    ) {
      this.imagePath = environment.imagesPath;
      this.users = new UserModel();
      this.filterModel = new FilterUserModel();
      this.tableColumns = EntityConstants.userTableColumns;
      this.statuses = EntityConstants.userState;
      this.pageSizeOptions = EntityConstants.pageSizeOptions;

      this.filterForm = this.formBuilder.group({
        searchString: [FilterConstants.emptyLine, Validators.minLength(3)],
        filteredColumnType: [UserFilteredColumnType.firstName],
        sortType: [AscendingDescendingSort.asc],
        pageSize: [FilterConstants.pageSize],
        pageCount: [FilterConstants.pageCount]
      });
      this.status = new FormControl(this.statuses);
     }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon(
      'pencil', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/pencil.svg`)).addSvgIcon(
      'delete', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/delete.svg`)).addSvgIcon(
      'search', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagePath}/search.svg`));
    if (!this.localStorageHelper.isRoleAdmin()) {
      this.router.navigate(['/printingEdition/userHomePage']);
    }
    this.getUsers();
  }

  setPaginationOptions($event: PageEvent): void {
    this.filterForm.controls.pageCount.setValue($event.pageIndex + 1);
    this.filterForm.controls.pageSize.setValue($event.pageSize);
    this.getUsers();
  }

  getUsers(): void {
    this.filterModel = this.filterForm.value;
    this.setUserStatus(this.status.value);
    this.userService.getAllUsers(this.filterModel).subscribe((data: UserModel) => {
      this.totalUsersCount = data.totalCount;
      this.users = data;
      });
  }
  setUserStatus(userStatus: string[]): void {
    if (userStatus.length === 2) {
      this.filterModel.status = UserStatus.All;
      return;
    }
    this.filterModel.status = UserStatus[userStatus[0]];
  }

  setAscendingSort(direction: string, column: string): void {
    this.filterForm.controls.sortType.setValue(AscendingDescendingSort[direction]);
    this.filterForm.controls.filteredColumnType.setValue(UserFilteredColumnType[column]);

    this.getUsers();
  }

  searchStringMinCharactersCheck(): void {
    if (this.filterForm.controls.searchString.value.length >= 3 ||
      this.filterForm.controls.searchString.value.length === 0) {
      this.getUsers();
    }
  }

  openEditDialog(id: number, firstName: string, lastName: string): void {
    const dialogRef = this.dialog.open(EditUserProfileComponent, {
      width: '500px',
      data: {id, firstName, lastName}
    });
    dialogRef.afterClosed().subscribe(() => {
        this.getUsers();
    });
  }

  openDeleteDialog(id: number): void {
    let nameEntityToDelete = NameEntityToDelete[NameEntityToDelete.User];
    const dialogRef = this.dialog.open(DeleteComponent, {
      width: '400px',
      data: {nameEntityToDelete}
    });
    dialogRef.afterClosed().subscribe((data: boolean) => {
      if (data === true ) {
        this.userService.deleteUser(id).subscribe(() => {
          this.getUsers();
        });
      }
    });
  }

  blockUser(id: number, blockState: boolean): void {
    this.userService.blockUser(id, blockState).subscribe();
  }
}


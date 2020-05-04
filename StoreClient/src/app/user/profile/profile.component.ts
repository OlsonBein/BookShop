import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

import { ValidationConstants, FilterConstants } from 'src/app/shared/constants';
import { UserModelItem, UserModel } from 'src/app/shared/models';
import { UserService, LocalStorageHelper } from 'src/app/shared/services';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: UserModelItem;
  updateForm: FormGroup;
  imagesPath: string;
  passwordPattern: string;
  @Input() userModel: UserModel;
  appearPhotoInput: boolean;
  selectedFile: File;
  confirmPassword: FormControl;
  image: any;

  constructor(
    private localStorageHelper: LocalStorageHelper,
    private userService: UserService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private formBuilder: FormBuilder
  ) {
    this.user = new UserModelItem();
    this.passwordPattern = ValidationConstants.passwordPattern;
    this.imagesPath = environment.imagesPath;
    this.appearPhotoInput = false;
    this.selectedFile = null;

    this.updateForm = formBuilder.group({
      firstName: [this.user.firstName, [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      lastName: [this.user.lastName, [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      email: [this.user.email, [Validators.required, Validators.email]],
      oldPassword: [FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]],
      newPassword: [FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]],
      confirmPassword: [FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]],
      avatar: [FilterConstants.emptyLine]
    });
   }

   ngOnInit(): void {
    this.iconRegistry.addSvgIcon('userIcon', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/userIcon.svg`));
    this.getUserData();
  }

  getUserData(): void {
    this.user = this.localStorageHelper.getUser();
    this.image = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.user.avatar}`);
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  matchPassword(): boolean {
    let result = this.updateForm.get('newPassword').value === this.updateForm.get('confirmPassword').value ? true : false;
    if (!result) {
      this.updateForm.controls.confirmPassword.setErrors(Validators.required);
    }
    return result;
  }

  editProfile(): void {
    this.user.oldPassword = this.updateForm.controls.oldPassword.value;
    this.user.newPassword = this.updateForm.controls.newPassword.value;
    this.user.avatar = this.updateForm.controls.avatar.value;
    this.userService.editUserProfile(this.user).subscribe(() => {
      this.localStorageHelper.saveUser(this.user);
      this.getUserData();
      this.refreshPage();
    });
  }

  refreshPage(): void {
    window.location.reload();
  }

  displayOrHideInput(): void {
    this.appearPhotoInput = !this.appearPhotoInput;
  }

  onFileSelected(event): void {
    this.selectedFile = event.target.files;
    let reader = new FileReader();
    reader.onload = this.handleReaderLoaded.bind(this);
    reader.readAsBinaryString(this.selectedFile[0]);
  }

  private handleReaderLoaded(readerEvent): void {
    let binaryString = readerEvent.target.result;
    this.updateForm.value.avatar = btoa(binaryString);
}

  setPhoto(): void {
    this.userService.setPhoto(this.updateForm.value).subscribe((data: UserModelItem) => {
      this.localStorageHelper.saveUser(data);
      this.getUserData();
      this.refreshPage();
    });
  }
}

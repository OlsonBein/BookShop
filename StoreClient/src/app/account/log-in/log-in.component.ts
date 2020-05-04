import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { LogInModel, UserModelItem } from 'src/app/shared/models';
import { AccountService} from 'src/app/shared/services/account.service';

import { ValidationConstants, FilterConstants, RoleConstants, EntityConstants } from 'src/app/shared/constants';
import { environment } from 'src/environments/environment';
import { LocalStorageHelper } from 'src/app/shared/services/local-storage.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
  providers: [AccountService]
})

export class LogInComponent implements OnInit {
  logInForm: FormGroup;
  logInModel: LogInModel;
  imagesPath: string;
  passwordPattern: string;
  currentUser: UserModelItem;

constructor(
  private iconRegistry: MatIconRegistry,
  private sanitizer: DomSanitizer,
  private accountService: AccountService,
  private localStorageHelper: LocalStorageHelper,
  private router: Router
  ) {
    this.passwordPattern = ValidationConstants.passwordPattern;
    this.imagesPath = environment.imagesPath;
    this.logInModel = new LogInModel();
    this.currentUser = new UserModelItem();
    this.logInForm = new FormGroup({
      email: new FormControl(FilterConstants.emptyLine, [Validators.required, Validators.email, Validators.minLength(4)] ),
      password: new FormControl(FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]),
      rememberMe: new FormControl(EntityConstants.defaultRememberMe)
    });
  }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('userIcon', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/userIcon.svg`));
    this.localStorageHelper.user.subscribe(data => {
      this.currentUser = data;
    });
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  logIn(): void {
    this.accountService.logIn(this.logInForm.value).subscribe(
      (data: UserModelItem) => {
        if (data.errors.length !== 0) {
          return;
        }
        this.localStorageHelper.saveUser(data);
        if (this.currentUser.role === RoleConstants.admin) {
          this.router.navigate(['/printingEdition/PrintingEditions']);
        }
        if (this.currentUser.role === RoleConstants.user) {
          this.router.navigate(['/printingEdition/userHomePage']);
        }
      });
  }
}


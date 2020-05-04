import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl  } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { AccountService } from 'src/app/shared/services/account.service';
import { FilterConstants } from 'src/app/shared/constants';

import { environment } from 'src/environments/environment';
import { BaseModel } from 'src/app/shared/models/base/base-model.models';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
  providers: [AccountService]
})
export class ResetPasswordComponent implements OnInit {
  resetForm: FormGroup;
  imagesPath: string;

  constructor(
    private router: Router,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private accountService: AccountService
    ) {
      this.imagesPath = environment.imagesPath;
      this.resetForm = new FormGroup({
        email: new FormControl(FilterConstants.emptyLine, [Validators.required, Validators.email])
      });
     }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('userIcon', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/userIcon.svg`));
  }

  resetPassword(): void {
    this.accountService.resetPassword(this.resetForm.controls.email.value).subscribe((data: BaseModel) => {
      if (data !== null) {
        this.router.navigate(['/account/logIn']);
      }
    });
  }
}

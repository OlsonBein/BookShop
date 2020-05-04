import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

import { RegistrationModel, BaseModel } from 'src/app/shared/models';
import { AccountService } from 'src/app/shared/services/account.service';
import { ValidationConstants, FilterConstants } from 'src/app/shared/constants';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [AccountService]
})
export class RegistrationComponent implements OnInit {
  registrationModel: RegistrationModel;
  userForm: FormGroup;
  imagesPath: string;
  passwordPattern: string;

  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router
    ) {
      this.passwordPattern = ValidationConstants.passwordPattern;
      this.imagesPath = environment.imagesPath;
      this.userForm = this.formBuilder.group({
        firstName: [FilterConstants.emptyLine, [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
        lastName: [FilterConstants.emptyLine, [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
        email: [FilterConstants.emptyLine, [Validators.required, Validators.email]],
        password: [FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]],
        confirmPassword: [FilterConstants.emptyLine, [Validators.required, Validators.pattern(this.passwordPattern)]]
        });

      this.registrationModel = new RegistrationModel();
     }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('userIcon', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/userIcon.svg`));
  }

  checkPasswordConfirmation(): boolean {
    let result = this.userForm.get('password').value === this.userForm.get('confirmPassword').value ? true : false;
    if (!result) {
      this.userForm.controls.confirmPassword.setErrors(Validators.required);
    }
    return result;
  }

  registrate(): void {
    this.registrationModel = this.userForm.value;
    this.accountService.registrate(this.registrationModel).subscribe((data: BaseModel) => {
      if (data !== null) {
        this.router.navigate(['/account/confirmEmail']);
      }
    });
  }
}

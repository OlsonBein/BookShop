import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { PrintingEditionDialogModel, AuthorModelItem } from 'src/app/shared/models';
import { PrintingEditionService, AuthorService } from 'src/app/shared/services';
import { ProductType, Currency, Action  } from 'src/app/shared/enums';
import { EntityConstants } from 'src/app/shared/constants';

@Component({
  selector: 'app-add-printing-edition',
  templateUrl: './add-printing-edition.component.html',
  styleUrls: ['./add-printing-edition.component.css'],
  providers: [PrintingEditionService]
})
export class AddPrintingEditionComponent implements OnInit {
  authors: Array<AuthorModelItem>;
  productTypes: string[];
  currency: string[];
  productForm: FormGroup;
  selectedProduct: string;
  selectedAuthorsControl: FormControl;

  constructor(
    private formBuilder: FormBuilder,
    private printingEditionService: PrintingEditionService,
    private authorService: AuthorService,
    private dialogRef: MatDialogRef<AddPrintingEditionComponent>,
    @Inject(MAT_DIALOG_DATA) public product: PrintingEditionDialogModel
  ) {
    this.authors = new Array<AuthorModelItem>();
    this.productTypes = EntityConstants.productTypes;
    this.currency = EntityConstants.currency;

    this.productForm = this.formBuilder.group({
      title: [this.product.printingEdition.title],
      description: [this.product.printingEdition.description],
      productType: [ProductType[this.product.printingEdition.productType]],
      currency: [Currency[this.product.printingEdition.currency]],
      price: [this.product.printingEdition.price],
      sale: [this.product.printingEdition.sale],
    });
    this.selectedAuthorsControl = new FormControl();
  }

  ngOnInit(): void {
    this.getAuthors();
  }

  clickCancel(): void {
    this.dialogRef.close();
  }

  chooseMethod(): void {
    this.product.printingEdition.description = this.productForm.controls.description.value;
    this.product.printingEdition.title = this.productForm.controls.title.value;
    this.product.printingEdition.price = this.productForm.controls.price.value;
    this.product.printingEdition.authors = this.selectedAuthorsControl.value;
    this.product.printingEdition.sale = this.productForm.controls.sale.value;
    if ( this.product.actionName === Action.Create) {
      this.createPrintingEdition();
      return;
    }
    this.updatePrintingEdition();
  }

  createPrintingEdition(): void {
    this.printingEditionService.createPrintingEdition(this.product.printingEdition).subscribe();
  }

  updatePrintingEdition(): void {
    this.printingEditionService.updatePrintingEdition(this.product.printingEdition).subscribe();
  }

  getAuthors(): void {
    this.authorService.getAuthors().subscribe(data => {
      this.authors = data.items;
      if (this.product.actionName === Action.Update) {
        this.selectedAuthorsControl = new FormControl(this.getSelectedAuthors());
      }
    });
  }

  setProductType(type: string): void {
    this.product.printingEdition.productType = ProductType[type];
  }

  setCurrency(element: string): void {
    this.product.printingEdition.currency = Currency[element];
  }

  getActionName(): string {
    return Action[this.product.actionName];
  }

  getSelectedAuthors(): Array<AuthorModelItem> {
    let selectedAuthors = new Array<AuthorModelItem>();
    if (this.product !== null) {
      this.product.printingEdition.authors.forEach(element => {
        this.authors.forEach(author => {
          if (author.name === element.name ) {
            selectedAuthors.push(author);
          }
        });
      });
    }
    return selectedAuthors;
  }
}

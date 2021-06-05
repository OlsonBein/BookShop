import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  imagesPath: string;

  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {
    this.imagesPath = environment.imagesPath;
  }

  ngOnInit(): void {
    this.iconRegistry.addSvgIcon('book', this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imagesPath}/bread.svg`));
  }

}

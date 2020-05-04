import { NgModule } from '@angular/core';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { MaterialModule } from 'src/app/material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from 'src/app/app.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FooterComponent } from 'src/app/shared/components/footer/footer.component';
import { HeaderComponent } from 'src/app/shared/components/header/header.component';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthenticationInterceptor } from 'src/app/shared/interceptors/authentication-interceptor';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
   declarations: [
      AppComponent,
      HeaderComponent,
      FooterComponent
   ],
   imports: [
      AppRoutingModule,
      BrowserAnimationsModule,
      MaterialModule,
      HttpClientModule,
      SharedModule
   ],
   providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
      },
      CookieService
   ],
   bootstrap: [
      AppComponent
   ]
})

export class AppModule { }

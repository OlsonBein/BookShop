import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, take } from 'rxjs/operators';

import { TokensModel } from 'src/app/shared/models';
import { AccountService, LocalStorageHelper } from 'src/app/shared/services';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  private isRefreshing;
  private refreshTokenSubject: BehaviorSubject<TokensModel>;

  constructor(
    private accountService: AccountService,
    private localStorageService: LocalStorageHelper
    ) {
      this.refreshTokenSubject = new BehaviorSubject<TokensModel>(null);
      this.isRefreshing = false;
     }

     intercept(request: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>>  {
      const tokens = this.localStorageService.getTokens();
      if (tokens !== null) {
        request = this.addTokensToHeader(request, tokens);
      }
      return next.handle(request).pipe(catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(request, next, tokens);
        }
        return throwError(error);
      }));
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler, tokens: TokensModel): Observable<any> {
      if (!this.isRefreshing) {
        this.isRefreshing = true;
        this.refreshTokenSubject.next(null);
        return this.refreshTokens(request, next, tokens);
       }
      return this.continueWithoutRefreshing(request, next, tokens);
    }

    private refreshTokens(request: HttpRequest<any>, next: HttpHandler, tokens: TokensModel): Observable<any> {
      return this.accountService.refreshTokens(tokens).pipe(
        switchMap(() => {
          this.isRefreshing = false;
          const newTokens = this.localStorageService.getTokens();
          this.refreshTokenSubject.next(newTokens);
          return next.handle(this.addTokensToHeader(request, newTokens));
         }));
    }

    private continueWithoutRefreshing(request: HttpRequest<any>, next: HttpHandler, tokens: TokensModel): Observable<any> {
      return this.refreshTokenSubject.pipe(
        take(1),
        switchMap(() => {
          return next.handle(this.addTokensToHeader(request, tokens));
        }));
    }

    private addTokensToHeader(request: HttpRequest<any>, tokens: TokensModel) {
      return request.clone({
        headers: request.headers.set('RefreshToken', tokens.refreshToken),
        setHeaders: {
          Authorization: `Bearer ${tokens.accessToken}`
        }
      });
    }
}

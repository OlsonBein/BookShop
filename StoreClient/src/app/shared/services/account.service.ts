import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { UserModelItem, LogInModel, RegistrationModel, TokensModel, BaseModel } from 'src/app/shared/models';
import { BaseService } from 'src/app/shared/services/base/base.service';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseService {
  apiUrl: string;

  constructor(
    private http: HttpClient
   ) {
      super();
      this.apiUrl = environment.apiUrl;
  }

  logIn(model: LogInModel): Observable<UserModelItem> {
    return this.http.post<UserModelItem>(`${this.apiUrl}/account/logIn`, model, this.getParams()).pipe(
      tap((data: UserModelItem) => {
        if (data.errors.length !== 0) {
          return this.checkServerErrors(data) as UserModelItem;
        }
        return data;
      })
    );
  }

  logOut(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/account/logOut`);
  }

  registrate(registrationModel: RegistrationModel): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/account/registrate`, registrationModel, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  resetPassword(email: string): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/account/resetPassword?email=${email}`, null, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  refreshTokens(tokens: TokensModel): Observable<TokensModel> {
    return this.http.post<TokensModel>(`${this.apiUrl}/account/refreshTokens`, {RefreshToken: tokens.refreshToken}, this.getParams());
  }
}

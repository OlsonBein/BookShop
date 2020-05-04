import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { FilterUserModel, UserModelItem, UserModel, BaseModel } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base/base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  apiUrl: string;

constructor(
  private http: HttpClient
) {
  super();
  this.apiUrl = environment.apiUrl;
  }

  getAllUsers(model: FilterUserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}/user/getAll`, model, this.getParams()).pipe(
      tap((data: UserModel) => {
        if (data.errors.length !== 0) {
          return  this.checkServerErrors(data);
        }
        return data;
      })
    );
  }

  deleteUser(id: number): Observable<BaseModel> {
    return this.http.get<BaseModel>(`${this.apiUrl}/user/delete?id=${id}`, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  blockUser(id: number, blockState: boolean): Observable<BaseModel> {
    return this.http.get<BaseModel>(`${this.apiUrl}/user/blockUser?id=${id}&block=${blockState}`, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  editUserProfile(model: UserModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/user/editProfile`, model, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  setPhoto(model: UserModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/user/setPhoto`, model, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }
}

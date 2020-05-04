import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { FilterAuthorModel, AuthorModel, AuthorModelItem, BaseModel } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base/base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorService extends BaseService {
  apiUrl: string;

  constructor(
    private http: HttpClient
  ) {
    super();
    this.apiUrl = environment.apiUrl;
   }

  getAllAuthors(model: FilterAuthorModel): Observable<AuthorModel> {
    return this.http.post<AuthorModel>(`${this.apiUrl}/author/getAll`, model, this.getParams()).pipe(
      tap((data: AuthorModel) => {
        if (data.errors.length !== 0) {
          return this.checkServerErrors(data) as AuthorModel;
        }
        return data;
      })
    );
  }

  createAuthor(model: AuthorModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/author/create`, model, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  updateAuthor(model: AuthorModelItem): Observable<BaseModel> {
    return this.http.post<BaseModel>(`${this.apiUrl}/author/update`, model, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  deleteAuthor(id: number): Observable<BaseModel>  {
    return this.http.get<BaseModel>(`${this.apiUrl}/author/delete?id=${id}`, this.getParams()).pipe(
      tap((data: BaseModel) => {
        return this.checkServerErrors(data);
      })
    );
  }

  getAuthors(): Observable<AuthorModel> {
    return this.http.get<AuthorModel>(`${this.apiUrl}/author/getAuthors`, this.getParams()).pipe(
      tap((data: AuthorModel) => {
        if (data.errors.length !== 0) {
          return this.checkServerErrors(data) as AuthorModel;
        }
        return data;
      })
    );
  }
}

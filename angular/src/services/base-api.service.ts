import { Injectable } from '@angular/core';
import { AppConsts } from '../shared/AppConsts';
import { HttpClient, HttpParams } from '@angular/common/http';
import { of as _observableOf, Observable } from 'rxjs';
import {
  mergeMap as observableMergeMap,
  catchError as observableCatch
} from 'rxjs/operators';
import { PagedRequestDto } from '../shared/paged-listing-component-base';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseApiService<T> {

  protected baseUrl = AppConsts.remoteServiceBaseUrl;
  protected http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }


  protected get rootApi() {
    return this.baseUrl + 'api/services/app/';
  }

  protected get url() {
    return this.baseUrl + '/api/services/app/' + this.name() + '/';
  }

  abstract name();

  getAll(request: PagedRequestDto): Observable<ResultDto<T>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    return this.http.get<any>(this.url + 'GetAll?' + requestQuery);
  }

  getDropdown(): Observable<any> {
    return this.http.get<any>(this.url + 'GetDropdown');
  }

  get(id: any): Observable<any> {
    return this.http.get(this.url + 'Get?id=' + id);
  }

  delete(id: any): Observable<any> {
    return this.http.delete<any>(this.url + 'Delete', {
      params: new HttpParams().set('Id', id)
    });
  }

  update(data: object): Observable<any> {
    return this.http.put(this.url + 'Update', data);
  }

  edit(data: object): Observable<any> {
    return this.http.post(this.url + 'Edit', data);
  }

  create(data: object): Observable<any> {
    return this.http.post(this.url + 'Create', data);
  }
}

export class ReponseDto {
  result: any | undefined;
  targetUrl: any | undefined;
  success: boolean | undefined;
  error: ErrorDto | undefined;
  message: any | undefined;
  unAuthorizedRequest: boolean | undefined;
  __abp: boolean | undefined;
}

export class ResonseGetItem<T> extends ReponseDto {
  result: T | undefined;
}

export class ReponseGetAllItem<T> extends ReponseDto {
  result: ResultDto<T> | undefined;
}

export class ErrorDto {
  code: number | undefined;
  message: string | undefined = 'Error';
  details: any | undefined;
  validationErrors: any | undefined;
}

export class ResultDto<T> {
  result: {
    totalCount: number | undefined,
    items: T[] | undefined
  };
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedRequestDto } from '../../shared/paged-listing-component-base';
import {BaseApiService, ResultDto} from '../base-api.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseApiService<any> {
  constructor(http: HttpClient) {
      super(http);
  }
  name = () => 'Category';

  search(request: PagedRequestDto, keyword: string): Observable<ResultDto<any>> {
      const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}&keyword=${keyword}`;
      return this.http.get<any>(this.url + 'GetAll?' + requestQuery);
    }
}

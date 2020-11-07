import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateProductDto } from '@app/product/dto/create-product-dto';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { Observable } from 'rxjs';
import { BaseApiService, ResultDto } from 'services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'ProductAdmin';

  getAllSeller(request: PagedRequestDto): Observable<ResultDto<any>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    return this.http.get<any>(this.url + 'GetAllSeller?' + requestQuery);
  }
}

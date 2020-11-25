import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { Observable } from 'rxjs';
import { BaseApiService, ResultDto } from 'services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'Invoice';

  GetMyInvoices(request: PagedRequestDto): Observable<ResultDto<any>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    return this.http.get<any>(this.url + 'GetMyInvoices?' + requestQuery);
  }

  UpdateAddress(item: any): Observable<ResultDto<any>> {
    return this.http.put<any>(this.url + 'UpdateAddress', item);
  }

  CompleteInvoice(item: any): Observable<ResultDto<any>> {
    return this.http.post<any>(this.url + 'CompleteInvoice', item);
  }
}

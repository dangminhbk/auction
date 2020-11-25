import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { Observable } from 'rxjs';
import { BaseApiService, ResultDto } from '../base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AuctionService extends BaseApiService<any> {


  constructor(http: HttpClient) {
    super(http);
  }

  name = () => 'Auction';

  getActiveAuction(request: PagedRequestDto): Observable<ResultDto<any>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    return this.http.get<any>(this.url + 'ListActiveAuction?' + requestQuery);
  }

  getAllForSeller(request: PagedRequestDto): Observable<ResultDto<any>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    return this.http.get<any>(this.url + 'GetAllForSeller?' + requestQuery);
  }

  getAllBids(request: PagedRequestDto, id: number): Observable<ResultDto<any>> {
    const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}&AuctionId=${id}`;
    return this.http.get<any>(this.url + 'GetBidsByAuctions?' + requestQuery);
  }

}

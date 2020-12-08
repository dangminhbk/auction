import { HttpClient, HttpParams } from '@angular/common/http';
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

  getActiveAuction(
    request: PagedRequestDto,
    minPrice: number,
    maxPrice: number,
    categoryIds: number[],
    brandId: number,
    keyword: string
  ): Observable<ResultDto<any>> {
    // const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    let params = new HttpParams();
    params = params.append('skipCount', request.skipCount.toString());
    params = params.append('maxResultCount', request.maxResultCount.toString());

    if (keyword !== null && keyword !== undefined) {
      params = params.append('keyword', keyword);
    }

    if (brandId !== null && brandId !== undefined) {
      params = params.append('brandId', brandId.toString());
    }

    if (maxPrice !== null && maxPrice !== undefined) {
      params = params.append('maxPrice', maxPrice.toString());
    }

    if (minPrice !== null && minPrice !== undefined) {
      params = params.append('minPrice', minPrice.toString());
    }

    if (categoryIds.length > 0) {
      categoryIds.forEach(s => {
        params = params.append('categoryIds', s.toString());
      });
    }

    console.log(params);

    return this.http.get<any>(this.url + 'ListActiveAuction', { params: params });
  }

  getActiveAuctionByShop(
    request: PagedRequestDto,
    sellerId: any
  ): Observable<ResultDto<any>> {
    // const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
    let params = new HttpParams();
    params = params.append('skipCount', request.skipCount.toString());
    params = params.append('maxResultCount', request.maxResultCount.toString());

    params = params.append('sellerId', sellerId.toString());

    return this.http.get<any>(this.url + 'ListActiveAuction', { params: params });
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

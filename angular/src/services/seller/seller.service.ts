import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SellerInfoDto } from '@app/seller-info/dto/seller-info-dto';
import { SellerUpdateDto } from '@app/seller-info/dto/seller-update-dto';
import { Observable } from 'rxjs';
import { BaseApiService, ResonseGetItem, ResultDto } from '../base-api.service';

@Injectable({
  providedIn: 'root'
})
export class SellerService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'Seller';
  getYourSeller(): Observable<ResonseGetItem<SellerInfoDto>> {
    return this.http.get<ResonseGetItem<SellerInfoDto>>(this.url + 'GetYourSellerInfo');
  }

  updateInfo(request: SellerUpdateDto): Observable<any> {
    return this.http.put(this.url + 'UpdateSellerInfo', request);
  }

  updatePayment() {

  }
}

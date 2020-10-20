import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterSellerDto } from 'account/register-seller/dto/register-seller-dto';
import { Observable } from 'rxjs';
import {BaseApiService} from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService extends BaseApiService<any> {
  name = ()=> 'Account';

  constructor(http: HttpClient) {
    super(http);
  }

  registerSeller(registerDto: RegisterSellerDto) : Observable<any> {
    return this.http.post<any>(this.url + 'RegisterSeller', registerDto);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';
@Injectable({
  providedIn: 'root'
})
export class CreditService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'PayIn';

  addCredit(data: any): Observable<any> {
    return this.http.post<any>(this.url + 'AddCredit', data);
  }
}
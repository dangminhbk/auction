import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class StatisticService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'Statistic';

  updateSetting(setting: any): Observable<any> {
    return this.http.post<any>(this.url + 'ChangeSetting', setting);
  }

  getDashboardData(): Observable<any> {
    return this.http.get(this.url + 'GetDashboardData');
  }

  getAdminDashboardData(): Observable<any> {
    return this.http.get(this.url + 'GetAminDashboardData');
  }

  getReportAllTime(): Observable<any> {
    return this.http.get(this.url + "GetSellerRevenueAllTimeReport", {
    })
  };

  getReportByMonth(year: number, month: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('year', year.toString());
    params = params.append('month', month.toString());
    return this.http.get(this.url + "GetSellerRevenueByMonthReport", {
      params: params
    })
  };

  getReportByYear(year: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('year', year.toString());
    return this.http.get(this.url + "GetSellerRevenueByYearReport", {
      params: params
    })
  };


  getAdminReportAllTime(): Observable<any> {
    return this.http.get(this.url + "GetAdminRevenueAllTimeReport", {
    })
  };

  getAdminReportByMonth(year: number, month: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('year', year.toString());
    params = params.append('month', month.toString());
    return this.http.get(this.url + "GetAdminRevenueByMonthReport", {
      params: params
    })
  };

  getAdminReportByYear(year: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('year', year.toString());
    return this.http.get(this.url + "GetAdminRevenueByYearReport", {
      params: params
    })
  };
}
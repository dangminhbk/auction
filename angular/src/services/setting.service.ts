import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class SettingService extends BaseApiService<any> {

  constructor(http: HttpClient) {
    super(http);
  }
  name = () => 'Configuration';

  getSetting() {
    console.log(abp.setting.values);
    return abp.setting;
  }

  updateSetting(setting: any): Observable<any> {
    return this.http.post<any>(this.url + 'ChangeSetting', setting);
  }
}

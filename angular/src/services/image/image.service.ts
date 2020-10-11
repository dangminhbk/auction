import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ImageDto } from "@app/images/dto/image-dto";
import { extend } from "lodash-es";
import { Observable } from "rxjs";
import {BaseApiService} from 'services/base-api.service';

@Injectable({
    providedIn: 'root'
})

export class ImageService extends BaseApiService<ImageDto> {
    constructor(http: HttpClient) {
        super(http);
    }
    name = () => 'Image';
    
    deleteSystem(id: any): Observable<any> {
        return this.http.delete<any>(this.url + 'DeleteSystem', {
          params: new HttpParams().set('Id', id)
        });
      }

    uploadSystem(form: FormData) : Observable<any> {
        return this.http.post<any>(this.url + 'Upload', form);
    }
}
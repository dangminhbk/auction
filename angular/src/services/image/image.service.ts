import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ImageDto } from "@app/images/dto/image-dto";
import { PagedRequestDto } from "@shared/paged-listing-component-base";
import { extend } from "lodash-es";
import { Observable } from "rxjs";
import {BaseApiService, ResultDto} from 'services/base-api.service';

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

    deleteSeller(id: any): Observable<any> {
        return this.http.delete<any>(this.url + 'DeleteSeller', {
          params: new HttpParams().set('Id', id)
        });
      }

    uploadSystem(form: FormData): Observable<any> {
        return this.http.post<any>(this.url + 'Upload', form);
    }

    uploadSeller(form: FormData) : Observable<any> {
        return this.http.post<any>(this.url + 'UploadSeller', form);
    }

    uploadCK(form: FormData): Promise<any> {
        return this.http.post<any>(this.url + 'UploadCK', form ).toPromise();
    }

    getAllSeller(request: PagedRequestDto): Observable<ResultDto<ImageDto>> {
        const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}`;
        return this.http.get<any>(this.url + 'GetAllSeller?' + requestQuery);
    }
}

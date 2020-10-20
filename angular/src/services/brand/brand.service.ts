import {BaseApiService, ResultDto} from '../base-api.service';
import {BrandDto} from '../../app/brand/dto/brand-dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequestDto } from '@shared/paged-listing-component-base';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class BrandService extends BaseApiService<BrandDto> {
    constructor(http: HttpClient) {
        super(http);
    }
    name = () => 'Brand';

    search(request: PagedRequestDto, keyword: string): Observable<ResultDto<BrandDto>> {
        const requestQuery = `skipCount=${request.skipCount}&maxResultCount=${request.maxResultCount}&keyword=${keyword}`;
        return this.http.get<any>(this.url + 'GetAll?' + requestQuery);
      }
}
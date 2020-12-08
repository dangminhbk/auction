import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { SellerService } from 'services/seller/seller.service';
import { ShopDto } from './dto/shop-dto';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent extends PagedListingComponentBase<ShopDto> implements OnInit {

  maxSize = 12;
  keyword = '';

  constructor(
    private _sellerService: SellerService,
    injector: Injector,
  ) {
    super(injector);
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.keyword = this.keyword;
    this._sellerService.getAllPublicSeller(request)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ResultDto<ShopDto>) => {
        this.items = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: ShopDto): void {
  }

}

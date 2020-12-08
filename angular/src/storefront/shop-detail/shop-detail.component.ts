import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SellerInfoDto } from './dto/seller-info-dto';
import { AppComponentBase } from '@shared/app-component-base';
import { SellerService } from 'services/seller/seller.service';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { AuctionListDto } from 'storefront/auction/dto/auction-list-dto';
import { AuctionService } from 'services/auction/auction.service';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';

@Component({
  selector: 'app-shop-detail',
  templateUrl: './shop-detail.component.html',
  styleUrls: ['./shop-detail.component.css']
})
export class ShopDetailComponent extends PagedListingComponentBase<AuctionListDto> implements OnInit {

  seller: SellerInfoDto = new SellerInfoDto();
  coverUrl = '';
  constructor(
    injector: Injector,
    private _sellerService: SellerService,
    protected _router: ActivatedRoute,
    private _auctionService: AuctionService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.load();
    this.refresh();
  }

  load(): void {
    const sellerId = this._router.snapshot.paramMap.get('id');
    this._sellerService
      .getPublicSeller(sellerId)
      .subscribe(s => {
        this.seller = s.result;
        this.coverUrl = this.getImagePath(this.seller.coverUrl);
      }
      );
  }
  oneCountdownEnd(event) {
    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    const sellerId = this._router.snapshot.paramMap.get('id');
    this._auctionService.getActiveAuctionByShop(
      request,
      sellerId
    )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ResultDto<AuctionListDto>) => {
        this.items = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: AuctionListDto): void {
    throw new Error('Method not implemented.');
  }

}

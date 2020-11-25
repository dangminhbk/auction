import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { AuctionListDto } from './dto/auction-list-dto';
import { MobileHelper } from '@shared/helpers/IsMobileDectector';
import { AuctionService } from 'services/auction/auction.service';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import * as dayjs from 'dayjs';
import { BrandDto } from '@app/brand/dto/brand-dto';
import { BrandService } from 'services/brand/brand.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent extends PagedListingComponentBase<AuctionListDto> {

  maxSize = 12;
  keyword = '';
  brands: BrandDto[] = [];

  constructor(
    private _auctionService: AuctionService,
    private _brandService: BrandService,
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit() {
    this._brandService
    .getDropdown()
    .subscribe(s => {
      this.brands = [...s.result];
      this.getDataPage(this.pageNumber);
    });
  }

  calculateEndTime(date: Date) {
    return dayjs(date).unix();
  }

  oneCountdownEnd(event) {
    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._auctionService.getActiveAuction(request)
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

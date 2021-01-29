import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { AuctionService } from 'services/auction/auction.service';
import { ResultDto } from 'services/base-api.service';
import { AuctionListDto } from 'storefront/auction/dto/auction-list-dto';
import * as dayjs from 'dayjs';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css'],
  animations: [appModuleAnimation()]
})
export class AuctionComponent extends PagedListingComponentBase<AuctionListDto> {

  brands: AuctionListDto[] = [];
  keyword = '';
  dayjs = dayjs;

  constructor(
    protected _auctionService: AuctionService,
    injector: Injector,
  ) {
    super(injector);
  }

  createItem() {
  }

  isActive(value: Date): boolean {
    const today = dayjs(new Date());
    const day = dayjs(value + 'Z');
    return (today.isBefore(day)) ? true : false;
  }

  CreateInvoice() {

  }


  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._auctionService.getAllForSeller(request)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ResultDto<AuctionListDto>) => {
        this.brands = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: AuctionListDto): void {
  }


}

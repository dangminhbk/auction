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
import { CategoryService } from 'services/category/category.service';
import { CategoryDto } from '@app/category/Dto/category-dto';
import { LabelType, Options } from '@angular-slider/ngx-slider';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent extends PagedListingComponentBase<AuctionListDto> implements OnInit {

  maxValue = 10000;
  minValue = 0;
  options: Options = {
    floor: 0,
    ceil: 10000,
    step: 10,
    selectionBarGradient: {
      from: 'white',
      to: '#FC0'
    },
    translate: (value: number, label: LabelType): string => {
      const prettyValue = this.currencyPipe.transform(value * 10000, 'VND');
      switch (label) {
        case LabelType.Low:
          return '<b>Thấp:</b>' + prettyValue;
        case LabelType.High:
          return '<b>Cao:</b>' + prettyValue;
        default:
          return prettyValue;
      }
    }
  };


  price = 10;

  maxSize = 12;
  keyword = '';
  brands: BrandDto[] = [];
  categories: CategoryDto[] = [];
  brandId = undefined;

  selectedCategories: number[] = [];

  constructor(
    private _auctionService: AuctionService,
    private _brandService: BrandService,
    private _categoryService: CategoryService,
    private currencyPipe: CurrencyPipe,
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

    this._categoryService
      .getDropdown()
      .subscribe(s => {
        this.categories = [...s.result];
        this.getDataPage(this.pageNumber);
      });
  }

  calculateEndTime(date: Date) {
    return dayjs(date).unix();
  }

  oneCountdownEnd(event) {
    this.refresh();
  }

  onChangeCategory(id: number, event) {
    console.log(event.target.checked);
    if (event.target.checked) {
      if (!this.selectedCategories.find(s => s === id)) {
        this.selectedCategories.push(id);
      }
    } else {
        this.selectedCategories = this.selectedCategories.filter(s => s !== id);
    }

    console.log(this.selectedCategories);
  }

  isChecked(id) {
    return this.selectedCategories.find(s => s === id) !== undefined;
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._auctionService.getActiveAuction(
      request,
      this.minValue * 10000,
      this.maxValue * 10000,
      this.selectedCategories,
      this.brandId,
      this.keyword
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

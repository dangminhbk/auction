import { Component, Injector, OnInit } from '@angular/core';
import { BrandDto } from '@app/brand/dto/brand-dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { BrandService } from 'services/brand/brand.service';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.css']
})
export class BrandComponent extends PagedListingComponentBase<BrandDto> implements OnInit {

  maxSize = 12;
  keyword = '';

  constructor(
    private _catService: BrandService,
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.keyword = this.keyword;
    this._catService.getDropdown()
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: any) => {
        this.items = result.result;
        // this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: BrandDto): void {
  }

}


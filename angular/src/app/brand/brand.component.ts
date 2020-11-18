import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BrandDto } from './dto/brand-dto';
import { BrandService } from 'services/brand/brand.service';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.css'],
  animations: [appModuleAnimation()]
})
export class BrandComponent extends PagedListingComponentBase<BrandDto> {

  brands: BrandDto[] = [];
  keyword = '';

  constructor(
    private _brandService: BrandService,
    injector: Injector,
  ) {
    super(injector);
  }

  createItem() {
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._brandService.search(request, this.keyword)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ResultDto<BrandDto>) => {
        this.brands = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: BrandDto): void {
    this._brandService.delete(entity.id)
      .subscribe();

    abp.message.confirm(
      'Do you want to delete?',
      undefined,
      (result: boolean) => {
        if (result) {
          this._brandService.delete(entity.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

}

import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ImageService } from 'services/image/image.service';
import { ProductListDto } from './dto/product-list-dto';
import { ProductService} from 'services/product/product.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { CreateAuctionComponent } from '@app/auction/create-auction/create-auction.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  animations: [appModuleAnimation()]
})
export class ProductComponent extends PagedListingComponentBase<ProductListDto> {

  advancedFiltersVisible = false;
  isActive: boolean | null;
  constructor(
    private imageService: ImageService,
    private productService: ProductService,
    injector: Injector,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createAuction(product: ProductListDto) {
    const createDialog = this._modalService.show(CreateAuctionComponent, {initialState: {
      product: product
    }});

    createDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.productService.getAllSeller(request)
    .pipe(
      finalize(() => {
        finishedCallback();
      })
    )
    .subscribe((result: ResultDto<ProductListDto>) => {
      this.items = result.result.items;
      this.showPaging(result.result, pageNumber);
    });
  }
  protected delete(entity: any): void {
  }

}


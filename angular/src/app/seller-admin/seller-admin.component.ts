import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { SellerService } from 'services/seller/seller.service';
import {SellerListDto} from './dto/seller-list-dto';
import { SellerAdminDetailComponent } from './seller-admin-detail/seller-admin-detail.component';
import { SellerAdminPaymentDetailComponent } from './seller-admin-payment-detail/seller-admin-payment-detail.component';

@Component({
  selector: 'app-seller-admin',
  templateUrl: './seller-admin.component.html',
  styleUrls: ['./seller-admin.component.css'],
  animations: [appModuleAnimation()]
})
export class SellerAdminComponent extends PagedListingComponentBase<SellerListDto> {

  sellers: SellerListDto[] = [];
  keyword = "";

  constructor(
    private _sellerService: SellerService,
    injector: Injector,
    private _modalService: BsModalService
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
    .subscribe((result: ResultDto<SellerListDto>) => {
      this.sellers = result.result.items;
      this.showPaging(result.result, pageNumber);
    });  }
  protected delete(entity: SellerListDto): void {
  }

  createItem() {
  }

  detail(sellerId: number) {
    const createDialog = this._modalService.show(SellerAdminDetailComponent, {
      initialState: {
        id: sellerId
      }
    });
  }

  paymentDetail (sellerId: number) {
    const createDialog = this._modalService.show(SellerAdminPaymentDetailComponent, {
      initialState: {
        id: sellerId
      }
    });
  }

}

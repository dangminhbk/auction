import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { PermissionCheckerService } from 'abp-ng2-module';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AuctionService } from 'services/auction/auction.service';
import { ResultDto } from 'services/base-api.service';
import { AuctionListDto } from 'storefront/auction/dto/auction-list-dto';
import { PopupComponent } from './popup/popup.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends PagedListingComponentBase<AuctionListDto> implements OnInit {

  setting: any = {
    mainBanner: {},
    seccondBanner: {},
    thirdBanner: {},
    promo: {}
  };
  constructor(
    injectot: Injector,
    private bsModal: BsModalService,
    private _auctionService: AuctionService,
    private _permissionChecker: PermissionCheckerService,
  ) {
    super(injectot);
  }

  ngOnInit(): void {

    const settingAll = abp.setting;
    this.setting = {
      mainBanner: {
        link: settingAll.get("MainBanner.Link"),
        headline: settingAll.get("MainBanner.Headline"),
        text: settingAll.get("MainBanner.Text"),
        image: settingAll.get("MainBanner.Image")
      },
      secondBanner: {
        link: settingAll.get("SeccondBanner.Link"),
        headline: settingAll.get("SeccondBanner.Headline"),
        text: settingAll.get("SeccondBanner.Text"),
        image: settingAll.get("SeccondBanner.Image")
      },
      thirdBanner: {
        link: settingAll.get("ThirdBanner.Link"),
        headline: settingAll.get("ThirdBanner.Headline"),
        text: settingAll.get("ThirdBanner.Text"),
        image: settingAll.get("ThirdBanner.Image")
      },
      promo: {
        link: settingAll.get("Promo.Link"),
        image: settingAll.get("Promo.Image"),
        isPromo: settingAll.getBoolean("Promo.On")
      }

    }
    if (this.setting.promo.isPromo) {
      const modalRef = this.bsModal.show(PopupComponent, {
        class: 'modal-dialog-centered popup-modal', initialState: {
          image: this.setting.promo.image,
          link: this.setting.promo.link
        }
      });
    }

    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.maxResultCount = 4;
    this._auctionService.getActiveAuction(
      request,
      null,
      null,
      null,
      null,
      null
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
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }

  oneCountdownEnd(event) {
    this.refresh();
  }

}

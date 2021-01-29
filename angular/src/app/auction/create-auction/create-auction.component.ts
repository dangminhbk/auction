import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { ProductListDto } from '@app/product/dto/product-list-dto';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AuctionService } from 'services/auction/auction.service';
import { CreateAuction } from '../dto/create-auction';
import * as dayjs from 'dayjs';
import * as utc from 'dayjs/plugin/utc';

@Component({
  selector: 'app-create-auction',
  templateUrl: './create-auction.component.html',
  styleUrls: ['./create-auction.component.css'],
  animations: [appModuleAnimation()]
})
export class CreateAuctionComponent extends AppComponentBase implements OnInit {

  auction: CreateAuction = new CreateAuction();
  saving = false;

  @Output() onSave = new EventEmitter<any>();
  product: any;

  constructor(
    injector: Injector,
    private auctionService: AuctionService,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.auction.initPrice = this.product.price;
    this.auction.minAcceptPrice = this.product.price;
    const date = dayjs().format('YYYY-MM-DDTHH:mm');
    this.auction.startDateString = date;
    this.auction.productId = this.product.id;
  }

  save(): void {
    abp.ui.setBusy();
    dayjs.extend(utc);
    const  observerble = this.auctionService.create(this.auction);
    // console.log(dayjs('2020-11-19T07:00').utc().format('YYYY-MM-DDTHH:mm:ss'));
    // console.log(dayjs('2020-11-19T06:00').utc().format('YYYY-MM-DDTHH:mm:ss'));
    this.auction.startDate = dayjs(this.auction.startDateString).utc().format('YYYY-MM-DDTHH:mm:ss') as any;
    this.auction.endDate = dayjs(this.auction.endDateString).utc().format('YYYY-MM-DDTHH:mm:ss') as any;
    observerble.
    subscribe(s => {
      this.notify.info(this.l('SavedSuccessfully'));
      setTimeout(() => {
        abp.ui.clearBusy();
        this.bsModalRef.hide();
      }, 1000);
    }, err => {
      abp.ui.clearBusy();
      this.notify.error(err.error.error.message, 'Update failed!');
    });
  }
}

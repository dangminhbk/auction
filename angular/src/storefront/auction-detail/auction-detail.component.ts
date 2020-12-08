import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AuctionService } from 'services/auction/auction.service';
import { ProductService } from 'services/product/product.service';
import { ProductDetailDto } from './dto/product-detail-dto';
import { AuctionDetailDto } from './dto/auction-detail-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { mergeMap } from 'rxjs/operators';
import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
import * as dayjs from 'dayjs';
import { SellerService } from 'services/seller/seller.service';
import { forkJoin, zip } from 'rxjs';
import { SellerInfoDto } from 'storefront/shop-detail/dto/seller-info-dto';
@Component({
  selector: 'app-auction-detail',
  templateUrl: './auction-detail.component.html',
  styleUrls: ['./auction-detail.component.css']
})
export class AuctionDetailComponent extends AppComponentBase implements OnInit {

  bidPrices: number;
  product: ProductDetailDto = new ProductDetailDto();
  auction: AuctionDetailDto = new AuctionDetailDto();
  seller: SellerInfoDto = new SellerInfoDto();

  constructor(
    injector: Injector,
    protected _productService: ProductService,
    protected _auctionService: AuctionService,
    protected _router: ActivatedRoute,
    protected _seller: SellerService,
    protected router: Router
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.loadData();
    this.initSignalR();
  }

  initSignalR() {
    const auctionId = this._router.snapshot.paramMap.get('id');
    let chatHub = null;

    SignalRAspNetCoreHelper.initSignalR(() => {

      abp.signalr.startConnection('/signalr-auction', (connection) => {
        chatHub = connection; // Save a reference to the hub

        chatHub.on('getMessage', (message) => {
          console.log('received message: ' + message);
          abp.notify.info('received message: ' + message);
        });

        chatHub.on('NewBidIsValid', (message) => {
          console.log(message);
          abp.notify.info(`Người dùng ${message.username} vừa đặt giá mới`);
          abp.event.trigger('auction.newPrice', {
            price: message.price,
            auctionId: message.auctionId,
            username: message.username,
            bidTime: message.bidTime
          });
        });

        chatHub.on('BidSuccess', (message) => {
          abp.notify.success('Đặt giá thành công');
        });

        chatHub.on('BidFailed', function (message) {
          abp.notify.error('Đặt giá thất bại');
        });
      }).then((connection) => {
        abp.notify.info('Kết nối đấu giá thành công');
        chatHub.invoke('JoinAuction', { Id: parseInt(auctionId) });
        // abp.log.debug('Connected to myChatHub server!');
        // abp.event.trigger('myChatHub.connected');
      });

      abp.event.on('auction.placeBid', function (bid) {
        chatHub.invoke('CallPrice', {
          Price: bid.price,
          AuctionId: bid.auctionId
        });
      });
    });

    abp.event.on('auction.newPrice', (bid) => {
      if (bid.auctionId === parseInt(auctionId)) {
        this.newBid(bid);
      }
    });
  }
  loadData() {
    abp.ui.setBusy();
    const auctionId = this._router.snapshot.paramMap.get('id');
    this._auctionService
      .get(auctionId)
      .pipe(mergeMap(s => {
        this.auction = s.result;

        const getProduct = this._productService.get(this.auction.productId);
        const getSeller = this._seller.getPublicSeller(this.auction.sellerId);
        return forkJoin({ product: getProduct, seller: getSeller });
        // return this._productService.get(this.auction.productId);
      }))
      .subscribe(s => {
        this.product = s.product.result;
        this.seller = s.seller.result;
        abp.ui.clearBusy();
        this.endAution();

      }, er => {
        abp.ui.clearBusy();
        this.router.navigate(['/storefront/auction']);
      });

  }

  placeBid() {
    const userId = this.appSession.userId;
    console.log(userId);
    if (userId === undefined || userId === null) {
      abp.message.info('Bạn phải đăng nhập để tham gia phiên đấu giá');
      return;
    }

    abp.event.trigger('auction.placeBid', {
      price: this.bidPrices,
      auctionId: this.auction.id
    });

  }

  isActive(value: Date): boolean {
    const today = dayjs(new Date());
    const day = dayjs(value + 'Z');
    return (today.isBefore(day)) ? true : false;
  }


  newBid(bid) {
    console.log(bid);
    this.auction.currentPrice = bid.price;
    this.auction.userName = bid.username;
    this.auction.lastBidTime = bid.bidTime;
  }

  protected endAution() {
    if (!this.isActive(this.auction.endDate)) {
      abp.notify.warn('Phiên đấu giá đã kết thúc');
      this.router.navigate(['/storefront/auction']);
    }
  }
}

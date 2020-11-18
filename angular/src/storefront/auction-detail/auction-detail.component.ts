import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AuctionService } from 'services/auction/auction.service';
import { ProductService } from 'services/product/product.service';
import { ProductDetailDto } from './dto/product-detail-dto';
import { AuctionDetailDto } from './dto/auction-detail-dto';
import { ActivatedRoute } from '@angular/router';
import { mergeMap } from 'rxjs/operators';
import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
@Component({
  selector: 'app-auction-detail',
  templateUrl: './auction-detail.component.html',
  styleUrls: ['./auction-detail.component.css']
})
export class AuctionDetailComponent extends AppComponentBase implements OnInit {

  bidPrices: number;
  product: ProductDetailDto = new ProductDetailDto();
  auction: AuctionDetailDto = new AuctionDetailDto();
  constructor(
    injector: Injector,
    protected _productService: ProductService,
    protected _auctionService: AuctionService,
    protected _router: ActivatedRoute
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
                auctionId: message.auctionId
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
        this.auction.currentPrice = bid.price;
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
        console.log(this.auction);
        return this._productService.get(this.auction.productId);
      }))
      .subscribe(s => {
        this.product = s.result;

        console.log(this.product);
        console.log(this.auction);
        abp.ui.clearBusy();
      }, er => {
        abp.ui.clearBusy();
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

  newBid(price) {
    this.auction.currentPrice = price;
  }
}

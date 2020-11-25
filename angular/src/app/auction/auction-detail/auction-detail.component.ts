import { Component, Injector, OnInit } from '@angular/core';
import { AuctionDetail } from '../dto/auction-detail';
import { AuctionDetailComponent as AuctionDetailHome } from 'storefront/auction-detail/auction-detail.component';
import { ProductService } from 'services/product/product.service';
import { AuctionService } from 'services/auction/auction.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BidListDto } from '../dto/bid-list-dto';
import { PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { finalize, mergeMap } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { InvoiceService } from 'services/invoice/invoice.service';
import * as dayjs from 'dayjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CreateAuctionComponent } from '../create-auction/create-auction.component';

@Component({
  selector: 'app-auction-detail',
  templateUrl: './auction-detail.component.html',
  styleUrls: ['./auction-detail.component.css']
})
export class AuctionDetailComponent extends AuctionDetailHome implements OnInit {

  public pageSize = 10;
  public pageNumber = 1;
  public totalPages = 1;
  public totalItems: number;
  public isTableLoading = false;

  public serialNumber: string;
  bids: BidListDto[] = [];
  constructor(
    injector: Injector,
    productService: ProductService,
    auctionService: AuctionService,
    protected _invoiceService: InvoiceService,
    private _modalService: BsModalService,
    router: ActivatedRoute,
    route: Router
  ) {
    super(
      injector,
      productService,
      auctionService,
      router,
      route
    );
  }

  ngOnInit(): void {
    this.loadData();
    this.loadBids();
  }

  loadBids() {
    this.getDataPage(this.pageNumber);
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
        this.router.navigate(['/storefront/auction']);
      });

  }

  isActive(value: Date): boolean {
    const today = dayjs(new Date());
    const day = dayjs(value);
    return (today.isBefore(day)) ? true : false;
  }

  makeInvoice() {
    abp.message.confirm('Bạn có muốn lập hóa đơn cho phiên đấu giá không ?', 'Thông báo',
      (result) => {
        if (result) {
          const invoice = {
            productName: this.product.name,
            subTotal: this.auction.currentPrice,
            auctionId: this.auction.id,
            serialNumber: this.serialNumber
          };
          abp.ui.setBusy();
          this._invoiceService
            .create(invoice)
            .subscribe(s => {
              abp.notify.success('Tạo hóa đơn thành công', 'Thành công');
              abp.ui.clearBusy();
            }, err => {
              abp.notify.error(err.error.error.message, 'Thất bại');
              abp.ui.clearBusy();
            });
        }
      }
    );
  }

  public showPaging(result: PagedResultDto, pageNumber: number): void {
    this.totalPages = ((result.totalCount - (result.totalCount % this.pageSize)) / this.pageSize) + 1;

    this.totalItems = result.totalCount;
    this.pageNumber = pageNumber;
  }

  public getDataPage(page: number): void {
    const req = new PagedRequestDto();
    req.maxResultCount = this.pageSize;
    req.skipCount = (page - 1) * this.pageSize;

    this.isTableLoading = true;
    this.list(req, page, () => {
      this.isTableLoading = false;
    });
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    const auctionId = this._router.snapshot.paramMap.get('id');
    this._auctionService.getAllBids(request, parseInt(auctionId))
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ResultDto<BidListDto>) => {
        this.bids = result.result.items;
        this.showPaging(result.result, pageNumber);
      });
  }

  protected endAuction() {
  }
}

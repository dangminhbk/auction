import { Component, Injector, OnInit } from '@angular/core';
import { AuctionDetail } from '../dto/auction-detail';
import { AuctionDetailComponent as AuctionDetailHome } from 'storefront/auction-detail/auction-detail.component';
import { ProductService } from 'services/product/product.service';
import { AuctionService } from 'services/auction/auction.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auction-detail',
  templateUrl: './auction-detail.component.html',
  styleUrls: ['./auction-detail.component.css']
})
export class AuctionDetailComponent extends AuctionDetailHome implements OnInit {

  constructor(
    injector: Injector,
    productService: ProductService,
    auctionService: AuctionService,
    router: ActivatedRoute
  ) {
    super(
      injector,
      productService,
      auctionService,
      router
    );
  }

  ngOnInit(): void {
    this.loadData();
    this.loadBids();
  }

  loadBids() {

  }
}

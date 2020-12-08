import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StorefrontComponent } from './storefront/storefront.component';

import { StoreFrontRoutingModule } from './storefront.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { NgxPaginationModule } from 'ngx-pagination';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { FooterComponent } from './layout/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ShopDetailComponent } from './shop-detail/shop-detail.component';
import { AuctionDetailComponent } from './auction-detail/auction-detail.component';
import { AuctionComponent } from './auction/auction.component';
import { CountdownModule } from 'ngx-countdown';
import { NgxCurrencyModule } from 'ngx-currency';
import { InvoiceComponent } from './invoice/invoice.component';
import { UpdateInvoiceAddressComponent } from './invoice/update-invoice-address/update-invoice-address.component';
import { InvoiceDetailComponent } from './invoice/invoice-detail/invoice-detail.component';
import { AuctionCountdownComponent } from './auction/auction-countdown/auction-countdown.component';
import { CategoryComponent } from './category/category.component';
import { LogoutComponent } from './logout/logout.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { CurrencyPipe } from '@angular/common';
import { Auction2Component } from './auction2/auction2.component';
import { BrandComponent } from './brand/brand.component';

@NgModule({
  declarations: [
    StorefrontComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    ShopComponent,
    ShopDetailComponent,
    AuctionDetailComponent,
    AuctionComponent,
    InvoiceComponent,
    UpdateInvoiceAddressComponent,
    InvoiceDetailComponent,
    AuctionCountdownComponent,
    CategoryComponent,
    LogoutComponent,
    Auction2Component,
    BrandComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    CommonModule,
    StoreFrontRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    CountdownModule,
    CarouselModule.forRoot(),
    NgxCurrencyModule,
    NgxSliderModule
  ],
  providers: [CurrencyPipe],
})
export class StorefrontModule { }

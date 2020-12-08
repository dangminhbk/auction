import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuctionDetailComponent } from './auction-detail/auction-detail.component';
import { AuctionComponent } from './auction/auction.component';
import { Auction2Component } from './auction2/auction2.component';
import { BrandComponent } from './brand/brand.component';
import { CategoryComponent } from './category/category.component';
import { HomeComponent } from './home/home.component';
import { InvoiceDetailComponent } from './invoice/invoice-detail/invoice-detail.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { LogoutComponent } from './logout/logout.component';
import { ShopDetailComponent } from './shop-detail/shop-detail.component';
import { ShopComponent } from './shop/shop.component';
import { StorefrontComponent } from './storefront/storefront.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: StorefrontComponent,
                children: [
                    {
                        path: 'home',
                        component: HomeComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'auction',
                        component: AuctionComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'auction/:id',
                        component: AuctionDetailComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'shop',
                        component: ShopComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'shop/:id',
                        component: ShopDetailComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'invoice',
                        component: InvoiceComponent
                    },
                    {
                        path: 'invoice/:id',
                        component: InvoiceDetailComponent
                    },
                    {
                        path: 'category',
                        component: CategoryComponent
                    },

                    {
                        path: 'brand',
                        component: BrandComponent
                    },

                    {
                        path: 'auction/category/:id',
                        component: Auction2Component,
                        children: [
                        ],
                        data: {
                            text: 'Danh mục:'
                        }
                    },

                    {
                        path: 'auction/brand/:id',
                        component: Auction2Component,
                        children: [
                        ],
                        data: {
                            text: 'Nhãn hiệu:',
                            isBrand: true
                        }
                    },

                    {
                        path: 'logout',
                        component: LogoutComponent,
                    }
                ]
            }

        ])
    ],
    exports: [RouterModule]
})
export class StoreFrontRoutingModule { }

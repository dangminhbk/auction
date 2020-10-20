import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { AuctionComponent } from "./auction/auction.component";
import { HomeComponent } from "./home/home.component";
import { ShopDetailComponent } from "./shop-detail/shop-detail.component";
import { ShopComponent } from "./shop/shop.component";
import {StorefrontComponent} from './storefront/storefront.component';

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
                        path: 'shop',
                        component: ShopComponent,
                        children: [
                        ]
                    },
                    {
                        path: 'shop-detail',
                        component: ShopDetailComponent,
                        children: [
                        ]
                    }
                ]
            }
            
        ])
    ],
    exports: [RouterModule]
})
export class StoreFrontRoutingModule { }
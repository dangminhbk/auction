import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ImagesComponent } from './images/images.component';
import { ImagesSellerComponent } from './images-seller/images-seller.component';
import { PermissionNames } from '@shared/const/PermissionNames';
import { BrandComponent } from './brand/brand.component';
import { CreateBrandComponent } from './brand/create-brand/create-brand.component';
import { SellerInfoComponent } from './seller-info/seller-info.component';
import { ProductComponent } from './product/product.component';
import { PaymentComponent } from './payment/payment.component';
import { PurchaseComponent } from './purchase/purchase.component';
import { AuctionComponent } from './auction/auction.component';
import { SellerAdminComponent } from './seller-admin/seller-admin.component';
import { CreateProductComponent } from './product/create-product/create-product.component';
import { AuctionDetailComponent } from './auction/auction-detail/auction-detail.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { InvoiceDetailComponent } from 'storefront/invoice/invoice-detail/invoice-detail.component';
import { StatisticComponent } from './statistic/statistic.component';
import { SettingComponent } from './setting/setting.component';
import { CategoryComponent } from './category/category.component';
import { CreateCatagoryComponent } from './create-catagory/create-catagory.component';
import { StatisticOrderComponent } from './statistic-order/statistic-order.component';
import { StatisticAdminRevenueComponent } from './statistic-admin-revenue/statistic-admin-revenue.component';
import { CreditHistoryComponent } from './credit-history/credit-history.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    // All user
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'about', component: AboutComponent },
                    // All user except buyer
                    { path: 'home', component: HomeComponent, canActivate: [AppRouteGuard] },
                    // Admin
                    {
                        path: 'sys-images', component: ImagesComponent,
                        data: { permission: PermissionNames.Admins }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'brands', component: BrandComponent,
                        data: { permission: PermissionNames.Admins }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'create-brand', component: CreateBrandComponent,
                        data: { permission: PermissionNames.Admins, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'edit-brand/:id', component: CreateBrandComponent,
                        data: { permission: PermissionNames.Admins, canActivate: [AppRouteGuard] }
                    },

                    {
                        path: 'setting', component: SettingComponent,
                        data: { permission: PermissionNames.Admins, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'category', component: CategoryComponent,
                        data: { permission: PermissionNames.Admins, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'category/create', component: CreateCatagoryComponent,
                        data: { permission: PermissionNames.Admins, canActivate: [AppRouteGuard] }
                    },

                    {
                        path: 'sellers', component: SellerAdminComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    // Seller
                    {
                        path: 'seller-info', component: SellerInfoComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'product', component: ProductComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'product/:id', component: CreateProductComponent,
                        data: { permission: PermissionNames.Seller, title: 'Sửa', isEdit: true, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'create-product', component: CreateProductComponent,
                        data: { permission: PermissionNames.Seller, title: 'Tạo sản phẩm', isEdit: false, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'payment-info', component: PaymentComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'invoice', component: InvoiceComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'invoice/:id', component: InvoiceDetailComponent,
                        data: { permission: PermissionNames.Seller, canActivate: [AppRouteGuard] }
                    },
                    {
                        path: 'seller-images', component: ImagesSellerComponent,
                        data: { permission: PermissionNames.Seller }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'auction', component: AuctionComponent,
                        data: { permission: PermissionNames.Seller }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'auction/:id', component: AuctionDetailComponent,
                        data: { permission: PermissionNames.Seller }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'statistic', component: StatisticComponent,
                        data: {
                            permission: PermissionNames.Seller,
                            forAdmin: false

                        }, canActivate: [AppRouteGuard]
                    },

                    {
                        path: 'statistic-admin', component: StatisticComponent,
                        data: {
                            permission: PermissionNames.Admins,
                            forAdmin: true

                        }, canActivate: [AppRouteGuard]
                    },

                    {
                        path: 'statistic/order', component: StatisticOrderComponent,
                        data: { permission: PermissionNames.Seller }, canActivate: [AppRouteGuard]
                    },

                    
                    {
                        path: 'statistic-admin/revenue', component: StatisticAdminRevenueComponent,
                        data: { permission: PermissionNames.Admins }, canActivate: [AppRouteGuard]
                    },

                    {
                        path: 'credit', component: CreditHistoryComponent,
                        data: { permission: PermissionNames.Admins }, canActivate: [AppRouteGuard]
                    },

                    // No use
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },

                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }

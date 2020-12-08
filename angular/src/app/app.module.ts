import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { ImagesComponent } from './images/images.component';
import { ImagesSellerComponent } from './images-seller/images-seller.component';
import { CreateImageComponent } from './images/create-image/create-image.component';
import { EditImageComponent } from './images/edit-image/edit-image.component';
import { BrandComponent } from './brand/brand.component';
import { CreateBrandComponent } from './brand/create-brand/create-brand.component';
import { ImagePickerComponent } from './images/image-picker/image-picker.component';
import { SellerInfoComponent } from './seller-info/seller-info.component';
import { PaymentComponent } from './payment/payment.component';
import { ProductComponent } from './product/product.component';
import { AuctionComponent } from './auction/auction.component';
import { PurchaseComponent } from './purchase/purchase.component';
import { SellerAdminComponent } from './seller-admin/seller-admin.component';
import { SellerAdminDetailComponent } from './seller-admin/seller-admin-detail/seller-admin-detail.component';
import { SellerAdminPaymentDetailComponent } from './seller-admin/seller-admin-payment-detail/seller-admin-payment-detail.component';
import { CreateProductComponent } from './product/create-product/create-product.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgxCurrencyModule } from 'ngx-currency';
import { CreateAuctionComponent } from './auction/create-auction/create-auction.component';
import { AuctionDetailComponent } from './auction/auction-detail/auction-detail.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { CreateInvoiceComponent } from './invoice/create-invoice/create-invoice.component';
import { InvoiceDetailComponent } from './invoice/invoice-detail/invoice-detail.component';
import { CategoryComponent } from './category/category.component';
import { StatisticComponent } from './statistic/statistic.component';
import { ChartsModule } from 'ng2-charts';
import { StatisticAuctionComponent } from './statistic-auction/statistic-auction.component';
import { StatisticOrderComponent } from './statistic-order/statistic-order.component';
import { StatisticProductComponent } from './statistic-product/statistic-product.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    // layout
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLanguageMenuComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    SidebarComponent,
    SidebarLogoComponent,
    SidebarUserPanelComponent,
    SidebarMenuComponent,
    // image
    ImagesComponent,
    ImagesSellerComponent,
    CreateImageComponent,
    EditImageComponent,
    BrandComponent,
    CreateBrandComponent,
    ImagePickerComponent,
    SellerInfoComponent,
    PaymentComponent,
    ProductComponent,
    AuctionComponent,
    PurchaseComponent,
    SellerAdminComponent,
    SellerAdminDetailComponent,
    SellerAdminPaymentDetailComponent,
    CreateProductComponent,
    CreateAuctionComponent,
    AuctionDetailComponent,
    InvoiceComponent,
    CreateInvoiceComponent,
    InvoiceDetailComponent,
    CategoryComponent,
    StatisticComponent,
    StatisticAuctionComponent,
    StatisticOrderComponent,
    StatisticProductComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    CKEditorModule,
    NgxCurrencyModule,
    ChartsModule
  ],
  providers: [],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
  ],
})
export class AppModule {}

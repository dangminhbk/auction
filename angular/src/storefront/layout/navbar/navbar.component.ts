import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { PermissionNames } from '@shared/const/PermissionNames';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent extends AppComponentBase implements OnInit {

  toggle = false;
  menuItems: MenuItem[] = [];
  constructor(
    injector: Injector
  ) {
    super(injector);
  }

  toggleClick() {
    this.toggle = !this.toggle;
    console.log(this.toggle);
  }

  ngOnInit(): void {
    if (this.isGranted(PermissionNames.Seller) || this.isGranted(PermissionNames.Admins)) {
      this.menuItems = [
        new MenuItem('Trang chủ', '/storefront/home', ''),
        new MenuItem('Quản trị', '/app', '')
      ];
    } else {
      let userName = this.appSession.getName();
      this.menuItems = [
        new MenuItem('Trang chủ', '/storefront/home', ''),
        new MenuItem('Đấu giá', '/storefront/auction', '')
      ];

      if (userName !== undefined) {
        this.menuItems.push(new MenuItem('Đơn hàng', '/storefront/invoice', ''));
        this.menuItems.push(new MenuItem('Đăng xuất', '/account/login', ''));
      } else {
        this.menuItems.push(new MenuItem('Đăng nhập', '/account/login', ''));
      }
    }

  }
}

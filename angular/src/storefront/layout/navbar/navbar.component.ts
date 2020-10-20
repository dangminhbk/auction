import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent extends AppComponentBase {

  toggle = false;
  menuItems: MenuItem[] = [];

  toggleClick() {
    this.toggle = !this.toggle;
    console.log(this.toggle);
  }
  constructor(
    injector: Injector
  ) {
    super(injector);
   }

  ngOnInit(): void {
    let userName = this.appSession.getName();
    this.menuItems = [
      new MenuItem('Trang chủ', '/storefront/home', ''),
      new MenuItem('Đấu giá', '/storefront/auction', ''),
      new MenuItem('Cửa hàng', '/storefront/shop', ''),
    ];
    console.log(userName);
    userName =  userName !== undefined ? userName : 'Đăng nhập';
    this.menuItems.push(new MenuItem(userName, '/account/login', ''));
  }
}

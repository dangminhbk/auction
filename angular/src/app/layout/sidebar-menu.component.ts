import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import {
  Router,
  RouterEvent,
  NavigationEnd,
  PRIMARY_OUTLET
} from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { MenuItem } from '@shared/layout/menu-item';
import { PermissionNames } from '@shared/const/PermissionNames';

@Component({
  selector: 'sidebar-menu',
  templateUrl: './sidebar-menu.component.html'
})
export class SidebarMenuComponent extends AppComponentBase implements OnInit {
  menuItems: MenuItem[];
  menuItemsMap: { [key: number]: MenuItem } = {};
  activatedMenuItems: MenuItem[] = [];
  routerEvents: BehaviorSubject<RouterEvent> = new BehaviorSubject(undefined);
  homeRoute = '/app/home';

  constructor(injector: Injector, private router: Router) {
    super(injector);
    this.router.events.subscribe(this.routerEvents);
  }

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
    this.patchMenuItems(this.menuItems);
    this.routerEvents
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        const currentUrl = event.url !== '/' ? event.url : this.homeRoute;
        const primaryUrlSegmentGroup = this.router.parseUrl(currentUrl).root
          .children[PRIMARY_OUTLET];
        if (primaryUrlSegmentGroup) {
          this.activateMenuItems('/' + primaryUrlSegmentGroup.toString());
        }
      });
  }

  getMenuItems(): MenuItem[] {
    return [
      // new MenuItem(
      //   this.l('Dashboard'),
      //   '/app/home',
      //   'fas fa-chart-line'),

      new MenuItem(
        this.l('Users'),
        '/app/users',
        'fas fa-users',
        'Pages.Users'
      ),
      new MenuItem(
        this.l('Roles'),
        '/app/roles',
        'fas fa-theater-masks',
        'Pages.Roles'
      ),

      // admin

      new MenuItem(
        this.l('Hình ảnh'),
        '/app/sys-images',
        'fas fa-image',
        PermissionNames.Admins
      ),

      new MenuItem(
        this.l('Nhãn hiệu'),
        '/app/brands',
        'fas fa-copyright',
        PermissionNames.Admins
      ),

      // seller

      // new MenuItem(
      //   this.l('Payment'),
      //   '/app/payment-info',
      //   'fas fa-money-bill-wave',
      //   PermissionNames.Seller
      // ),

      new MenuItem(
        'Cài đặt',
        '',
        'fas fa-cog',
        PermissionNames.Seller,
        [
          new MenuItem(
            this.l('Thông tin'),
            '/app/seller-info',
            'fas fa-info',
            PermissionNames.Seller
          ),
          new MenuItem(
            this.l('Hình ảnh'),
            '/app/seller-images',
            'fas fa-image',
            PermissionNames.Seller
          )
        ]
      ),

      new MenuItem(
        'Thống kê',
        '',
        'fas fa-chart-line',
        PermissionNames.Seller,
        [
          new MenuItem(
            this.l('Tổng quan'),
            '/app/statistic',
            'fas fa-minus',
            PermissionNames.Seller
          ),
          new MenuItem(
            this.l('Báo cáo doanh thu'),
            '/app/statistic/order',
            'fas fa-minus',
            PermissionNames.Seller
          )
        ]
      ),

      new MenuItem(
        'Thống kê',
        '',
        'fas fa-chart-line',
        PermissionNames.Admins,
        [
          new MenuItem(
            this.l('Tổng quan'),
            '/app/statistic-admin',
            'fas fa-minus',
            PermissionNames.Admins
          ),
          new MenuItem(
            this.l('Báo cáo doanh thu'),
            '/app/statistic-admin/revenue',
            'fas fa-minus',
            PermissionNames.Admins
          )
        ]
      ),

      new MenuItem(
        this.l('Sản phẩm'),
        '',
        'fas fa-store',
        PermissionNames.Seller,
        [new MenuItem(
          'Tạo mới',
          '/app/create-product',
          'fas fa-minus',
          PermissionNames.Seller,
        ), new MenuItem(
          'Danh sách',
          '/app/product',
          'fas fa-minus',
          PermissionNames.Seller,
        )]
      ),

      new MenuItem(
        this.l('Đấu giá'),
        '/app/auction',
        'fas fa-balance-scale',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Đơn hàng'),
        '/app/invoice',
        'fas fa-file-invoice-dollar',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Người bán hàng'),
        '/app/sellers',
        'fas fa-store',
        PermissionNames.Admins
      ),

      // new MenuItem(
      //   this.l('Danh mục'),
      //   '/app/category',
      //   'fas fa-folder',
      //   PermissionNames.Admins
      // ),

      new MenuItem(
        this.l('Thiết lập'),
        '/app/setting',
        'fas fa-cog',
        PermissionNames.Admins
      ),

      new MenuItem(
        this.l('Lịch sử nạp tiền'),
        '/app/credit',
        'fas fa-dollar-sign',
        PermissionNames.Admins
      ),
    ];
  }

  patchMenuItems(items: MenuItem[], parentId?: number): void {
    items.forEach((item: MenuItem, index: number) => {
      item.id = parentId ? Number(parentId + '' + (index + 1)) : index + 1;
      if (parentId) {
        item.parentId = parentId;
      }
      if (parentId || item.children) {
        this.menuItemsMap[item.id] = item;
      }
      if (item.children) {
        this.patchMenuItems(item.children, item.id);
      }
    });
  }

  activateMenuItems(url: string): void {
    this.deactivateMenuItems(this.menuItems);
    this.activatedMenuItems = [];
    const foundedItems = this.findMenuItemsByUrl(url, this.menuItems);
    foundedItems.forEach((item) => {
      this.activateMenuItem(item);
    });
  }

  deactivateMenuItems(items: MenuItem[]): void {
    items.forEach((item: MenuItem) => {
      item.isActive = false;
      item.isCollapsed = true;
      if (item.children) {
        this.deactivateMenuItems(item.children);
      }
    });
  }

  findMenuItemsByUrl(
    url: string,
    items: MenuItem[],
    foundedItems: MenuItem[] = []
  ): MenuItem[] {
    items.forEach((item: MenuItem) => {
      if (item.route === url) {
        foundedItems.push(item);
      } else if (item.children) {
        this.findMenuItemsByUrl(url, item.children, foundedItems);
      }
    });
    return foundedItems;
  }

  activateMenuItem(item: MenuItem): void {
    item.isActive = true;
    if (item.children) {
      item.isCollapsed = false;
    }
    this.activatedMenuItems.push(item);
    if (item.parentId) {
      this.activateMenuItem(this.menuItemsMap[item.parentId]);
    }
  }

  isMenuItemVisible(item: MenuItem): boolean {
    if (!item.permissionName) {
      return true;
    }
    return this.permission.isGranted(item.permissionName);
  }
}

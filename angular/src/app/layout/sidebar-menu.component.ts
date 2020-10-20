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
      new MenuItem(
        this.l('Dashboard'),
        '/app/home', 
        'fas fa-chart-line'),

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
        this.l('Images'),
        '/app/sys-images',
        'fas fa-image',
        PermissionNames.Admins
      ),

      new MenuItem(
        this.l('Brands'),
        '/app/brands',
        'fas fa-copyright',
        PermissionNames.Admins
      ),

      // seller
      new MenuItem(
        this.l('Images'),
        '/app/seller-images',
        'fas fa-image',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Payment'),
        '/app/payment-info',
        'fas fa-money-bill-wave',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Info'),
        '/app/seller-info',
        'fas fa-info',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Products'),
        '/app/product',
        'fas fa-store',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Auctions'),
        '/app/auction',
        'fas fa-balance-scale',
        PermissionNames.Seller
      ),

      new MenuItem(
        this.l('Invoices'),
        '/app/invoice',
        'fas fa-file-invoice-dollar',
        PermissionNames.Seller
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

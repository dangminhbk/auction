import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
import { PermissionCheckerService } from 'abp-ng2-module';

@Component({
  selector: 'app-storefront',
  templateUrl: './storefront.component.html',
  styleUrls: ['./storefront.component.css']
})
export class StorefrontComponent implements OnInit, OnDestroy {

  constructor(
    private _permissionChecker: PermissionCheckerService,
  ) { }

  ngOnInit(): void {
    if(this._permissionChecker.isGranted("Sellers")) {
      abp.ui.setBusy();
      location.href = "app/statistic";
    }

    if(this._permissionChecker.isGranted("Admins")) {
      abp.ui.setBusy();
      location.href = "app/statistic-admin";
    }
  }

  ngOnDestroy() {
    abp.ui.clearBusy();
  }

}

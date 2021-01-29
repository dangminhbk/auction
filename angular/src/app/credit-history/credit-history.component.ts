import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import {CreditService} from 'services/credit.service';

@Component({
  selector: 'app-credit-history',
  templateUrl: './credit-history.component.html',
  styleUrls: ['./credit-history.component.css']
})
export class CreditHistoryComponent extends PagedListingComponentBase<any> implements OnInit {

  keyword: string;
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._creditService.getAll(request)
    .pipe(
      finalize(() => {
        finishedCallback();
      })
    )
    .subscribe((result: any) => {
      this.items = result.result.items;
      this.showPaging(result.result, pageNumber);
    });  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }

  constructor(
    injector: Injector,
    private _creditService: CreditService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh();
  }

}

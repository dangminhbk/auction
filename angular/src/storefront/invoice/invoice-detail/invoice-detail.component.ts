import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { InvoiceService } from 'services/invoice/invoice.service';

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html',
  styleUrls: ['./invoice-detail.component.css']
})
export class InvoiceDetailComponent extends AppComponentBase implements OnInit {

  invoice: any = {};

  constructor(
    injector: Injector,
    private _invoiceService: InvoiceService,
    private _router: ActivatedRoute
  ) {
    super(injector);
  }

  ngOnInit(): void {
    abp.ui.setBusy();
    const id = this._router.snapshot.paramMap.get('id');
    this._invoiceService
      .get(parseInt(id))
      .subscribe(s => {
        this.invoice = s.result;
        console.log(s.result);
        abp.ui.clearBusy();
      });
  }

}

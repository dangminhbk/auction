import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { InvoiceService } from 'services/invoice/invoice.service';

@Component({
  selector: 'app-update-invoice-address',
  templateUrl: './update-invoice-address.component.html',
  styleUrls: ['./update-invoice-address.component.css']
})
export class UpdateInvoiceAddressComponent extends AppComponentBase implements OnInit {

  saving = false;
  invoice: any = {};
  @Input() id: number;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    public bsModalRef: BsModalRef,
    private _invoiceService: InvoiceService,
    injector: Injector
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.invoice.id = this.id;
  }

  save() {
    abp.ui.setBusy();
    this._invoiceService
    .UpdateAddress(this.invoice)
    .subscribe(s => {
      abp.ui.clearBusy();
      this.bsModalRef.hide();
      this.onSave.emit();
    }, err => {
      abp.ui.clearBusy();
      abp.notify.error('Cập nhập thất bại');
    });
  }

}

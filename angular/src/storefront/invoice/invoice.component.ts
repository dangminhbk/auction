import { Component, Injector, OnInit } from '@angular/core';
import { InvoiceListDto } from '@app/invoice/dto/invoice-list-dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { InvoiceService } from 'services/invoice/invoice.service';
import { UpdateInvoiceAddressComponent } from './update-invoice-address/update-invoice-address.component';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent extends PagedListingComponentBase<InvoiceListDto> implements OnInit {

  constructor(
    injector: Injector,
    protected _invoiceService: InvoiceService,
    protected _modalService: BsModalService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh();
  }

  getStatus(status: number) {
    switch (status) {
      case 0: return '<label class="bg-gray">Khởi tạo</label>';
      case 1: return '<label class="bg-gray">Chờ</p>';
      case 2: return '<label class="bg-green">Hoàn thành</label>';
      case 3: return '<label class="bg-red">Bị hủy</label >';
      default:
        break;
    }
  }

  updateAddress(id: number) {
    const ref = this._modalService.show(UpdateInvoiceAddressComponent, {initialState: {id: id}});
    ref.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._invoiceService.GetMyInvoices(request)
    .pipe(
      finalize(() => {
        finishedCallback();
      })
    )
    .subscribe((result: ResultDto<any>) => {
      this.items = result.result.items;
      this.showPaging(result.result, pageNumber);
    });
  }
  protected delete(entity: InvoiceListDto): void {
    throw new Error('Method not implemented.');
  }

}


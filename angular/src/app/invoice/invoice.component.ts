import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { InvoiceListDto } from './dto/invoice-list-dto';
import { InvoiceService } from 'services/invoice/invoice.service';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent extends PagedListingComponentBase<InvoiceListDto> implements OnInit {

  constructor(
    injector: Injector,
    protected _invoiceService: InvoiceService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.refresh();
  }

  completeInvoice(id: number) {
    abp.message.confirm('Bạn có muốn hoàn thành hóa đơn này không?', 'Thông báo', res => {
      if (res) {
        abp.ui.setBusy();
        this._invoiceService
          .CompleteInvoice({id: id})
          .subscribe(s => {
            abp.ui.clearBusy();
            abp.notify.success('Thành công');
            this.refresh();
          }, err => {
            abp.notify.error('Thất bại');
            abp.ui.clearBusy();
          });
      }
    });
  }

  canComplete(status: number) {
    if (status === 1) {
      return true;
    }
    return false;
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

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._invoiceService.getAll(request)
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

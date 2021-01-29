import { Component, Injector, Input, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CreditService } from 'services/credit.service';

@Component({
  selector: 'app-add-credit',
  templateUrl: './add-credit.component.html',
  styleUrls: ['./add-credit.component.css']
})
export class AddCreditComponent extends AppComponentBase implements OnInit {

  @Input() id;
  saving = false;
  credit: any = { money: 0};
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _creditService: CreditService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.credit.targetId = this.id;
  }

  save() {
    abp.ui.setBusy();
    this._creditService
      .addCredit(this.credit)
      .subscribe(
        s => {
          this.notify.info(this.l('Thành công'));
          setTimeout(() => {
            abp.ui.clearBusy();
            this.bsModalRef.hide();
          }, 1000);
        }, err => {
          abp.ui.clearBusy();
          this.notify.error(err.error.error.message, 'Lỗi');
        });
  }

}

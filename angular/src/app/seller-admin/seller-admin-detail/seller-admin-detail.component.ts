import { Component, Injector, Input, OnInit } from '@angular/core';
import { SellerInfoDto } from '@app/seller-info/dto/seller-info-dto';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { SellerService } from 'services/seller/seller.service';

@Component({
  selector: 'app-seller-admin-detail',
  templateUrl: './seller-admin-detail.component.html',
  styleUrls: ['./seller-admin-detail.component.css']
})
export class SellerAdminDetailComponent extends AppComponentBase implements OnInit {

  @Input() id: number;
  sellerInfo: SellerInfoDto = new SellerInfoDto();
  isEdit = false;
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _sellerService: SellerService
  ) {
    super(injector);
  }

  ngOnInit() {
    this._sellerService
    .getSellerById(this.id)
    .pipe(finalize(()=>
      {

    }))
    .subscribe(s=>{
      this.sellerInfo = s.result;
    })
  }
}

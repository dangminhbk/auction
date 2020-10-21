import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImageDto } from '@app/images/dto/image-dto';
import { ImagePickerComponent } from '@app/images/image-picker/image-picker.component';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { SellerInfoDto } from './dto/seller-info-dto';
import { SellerService } from 'services/seller/seller.service';
import { finalize } from 'rxjs/operators';
import { SellerUpdateDto } from './dto/seller-update-dto';

@Component({
  selector: 'app-seller-info',
  templateUrl: './seller-info.component.html',
  styleUrls: ['./seller-info.component.css']
})
export class SellerInfoComponent extends AppComponentBase {

  sellerInfo: SellerInfoDto = new SellerInfoDto();
  isEdit = false;

  constructor(
    _injector: Injector,
    private _modalService: BsModalService,
    private sellerService: SellerService
  ) {
    super(_injector);
   }

  ngOnInit(): void {
    this.loadData();
  }
  
  loadData() {
    this.sellerService
    .getYourSeller()
    .subscribe(s=> {
      this.sellerInfo = s.result;
    });
  }

  openImagePickerCover() {
    this.openImagePicker()
    .subscribe((s: ImageDto[]) => {
      if (s.length == 0) {
        this.sellerInfo.sellerCoverId = undefined;
      } else {
        this.sellerInfo.sellerCoverId = s[0].id;
        this.sellerInfo.sellerCoverUrl = s[0].url;
      }
    });
  }

  openImagePickerLogo() {

    this.openImagePicker()
    .subscribe((s: ImageDto[]) => {
      if (s.length == 0) {
        this.sellerInfo.sellerLogoId = undefined;
      } else {
        this.sellerInfo.sellerLogoId = s[0].id;
        this.sellerInfo.sellerLogoUrl = s[0].url;
      }
    });
  }

  openImagePicker(): Observable<any> {
    const createDialog = this._modalService.show(ImagePickerComponent,
      { 
        initialState: {
          pickMany: false,
          forAdmin: false
        }
      }
    );

    return createDialog.content.onSavePickedImage;
  }

  save() {
    const data = new SellerUpdateDto();
    data.sellerName = this.sellerInfo.name;
    data.sellerLogo = this.sellerInfo.sellerLogoId;
    data.sellerCover = this.sellerInfo.sellerCoverId;
    data.address = this.sellerInfo.address;
    data.description = this.sellerInfo.description;
    data.phoneNumber = this.sellerInfo.phoneNumber;
    
    abp.ui.block();
    this.sellerService
    .updateInfo(data)
    .pipe(finalize(() => {
      this.isEdit = false;
      abp.ui.unblock();
    }))
    .subscribe(s=> {
      abp.notify.success('Update success!');
      this.loadData();
    }, err=> {
      abp.notify.error('Update fail!');
    })
    ;
  }

}

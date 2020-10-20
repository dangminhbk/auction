import { Component, Injector, OnInit } from '@angular/core';
import { ImageDto } from '@app/images/dto/image-dto';
import { ImagePickerComponent } from '@app/images/image-picker/image-picker.component';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { SellerInfoDto } from './dto/seller-info-dto';

@Component({
  selector: 'app-seller-info',
  templateUrl: './seller-info.component.html',
  styleUrls: ['./seller-info.component.css']
})
export class SellerInfoComponent extends AppComponentBase {

  sellerInfo: SellerInfoDto = new SellerInfoDto();
  constructor(
    _injector: Injector,
    private _modalService: BsModalService,
  ) {
    super(_injector);
   }

  ngOnInit(): void {
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

}

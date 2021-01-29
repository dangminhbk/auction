import { Component, Injector, OnInit } from '@angular/core';
import { ImageDto } from '@app/images/dto/image-dto';
import { ImagePickerComponent } from '@app/images/image-picker/image-picker.component';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import {SettingService } from 'services/setting.service';


@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent extends AppComponentBase implements OnInit {

  setting: any = {
    mainBanner: {},
    seccondBanner: {},
    thirdBanner: {},
    promo: {}
  };
  isEdit = false;
  constructor(
    injector: Injector,
    private _settingService: SettingService,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    const settingAll = this._settingService.getSetting();

    this.setting = {
      mainBanner: {
        link: settingAll.get("MainBanner.Link"),
        headline: settingAll.get("MainBanner.Headline"),
        text: settingAll.get("MainBanner.Text"),
        image: settingAll.get("MainBanner.Image")
      },
      secondBanner: {
        link:     settingAll.get("SeccondBanner.Link"),
        headline: settingAll.get("SeccondBanner.Headline"),
        text:     settingAll.get("SeccondBanner.Text"),
        image:    settingAll.get("SeccondBanner.Image")
      },
      thirdBanner: {
        link:     settingAll.get("ThirdBanner.Link"),
        headline: settingAll.get("ThirdBanner.Headline"),
        text:     settingAll.get("ThirdBanner.Text"),
        image:    settingAll.get("ThirdBanner.Image")
      },
      promo: {
        link:     settingAll.get("Promo.Link"),
        image:     settingAll.get("Promo.Image"),
        isPromo:    settingAll.getBoolean("Promo.On")
      }
    }

    console.log(this.setting.promo);
  }

  save() {
    abp.ui.setBusy();
    this._settingService
    .updateSetting(this.setting)
    .subscribe(s=> {
      abp.notify.success("Thiết lập thành công");
      abp.ui.clearBusy();
    }, err=> {
      abp.notify.error("Thiết lập thất bại");
      abp.ui.clearBusy();
    })
   }

  openImagePicker() {
    const createDialog = this._modalService.show(ImagePickerComponent,
      {
        initialState: {
          pickMany: false,
          forAdmin: true
        }
      }
    );

    return createDialog.content.onSavePickedImage;
  }

  openBanner1Picker() {
    this.
      openImagePicker().subscribe((s: ImageDto[]) => {
        if (s.length == 0) {
          this.setting.mainBanner.image = undefined;
        } else {

          this.setting.mainBanner.image = s[0].url;
        }
      });
  }

  openPopupPicker() {
    this.
      openImagePicker().subscribe((s: ImageDto[]) => {
        if (s.length == 0) {
          this.setting.promo.image = undefined;
        } else {
          this.setting.promo.image = s[0].url;
        }
      });
  }

  openBanner3Picker() {
    this.
      openImagePicker().subscribe((s: ImageDto[]) => {
        if (s.length == 0) {
          this.setting.thirdBanner.image = undefined;
        } else {

          this.setting.thirdBanner.image = s[0].url;
        }
      });
  }

  openBanner2Picker() {
    this.
      openImagePicker().subscribe((s: ImageDto[]) => {
        if (s.length == 0) {
          this.setting.secondBanner.image = undefined;
        } else {

          this.setting.secondBanner.image = s[0].url;
        }
      });
  }
}

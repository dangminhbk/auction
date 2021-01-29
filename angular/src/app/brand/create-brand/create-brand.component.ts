import { Component, Injector, OnInit } from '@angular/core';
import { ImageDto } from '@app/images/dto/image-dto';
import { ImagePickerComponent } from '@app/images/image-picker/image-picker.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BrandDto } from '../dto/brand-dto';
import { CommonModule } from '@angular/common';
import { AppComponentBase } from '@shared/app-component-base';
import { Inject } from '@angular/compiler/src/core';
import { BrandService } from 'services/brand/brand.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-brand',
  templateUrl: './create-brand.component.html',
  styleUrls: ['./create-brand.component.css']
})
export class CreateBrandComponent extends AppComponentBase implements OnInit {

  brand: BrandDto = new BrandDto();
  isUpdate = false;
  title = 'Tạobrand';

  constructor(
    injecto: Injector,
    private _modalService: BsModalService,
    private brandService: BrandService,
    private router: Router,
    private activatedRouter: ActivatedRoute
  ) {
    super(injecto);
    if (router.url.includes('edit-brand')) {
      this.isUpdate = true;
      this.title = 'Edit brand';
    }
   }

  ngOnInit(): void {
    if (this.isUpdate) {
      const id = this.activatedRouter.snapshot.paramMap.get('id');
      this.brandService
      .get(id).subscribe(s => {
        this.brand = s.result as BrandDto;
      });
    }
  }

  openImagePicker(): void {
    const createDialog = this._modalService.show(ImagePickerComponent,
      {
        initialState: {
          pickMany: false
        }
      }
    );

    createDialog.content.onSavePickedImage.subscribe((s: ImageDto[]) => {
      if (s.length === 0) {
        this.brand.brandImageId = undefined;
      } else {
        this.brand.brandImageId = s[0].id;
        this.brand.brandImageUrl = s[0].url;
      }
      // console.log(this.brand);
    });
  }

  save() {
    abp.ui.setBusy();
    let observerble = new Observable<any>();
    if (this.isUpdate) {
      observerble = this.brandService.edit(this.brand);
    } else {
      observerble = this.brandService.create(this.brand);
    }
    observerble.
    subscribe(s => {
      this.notify.info(this.l('SavedSuccessfully'));
      setTimeout(() => {
        abp.ui.unblock();
        this.router.navigate(['/app/brands']);
      }, 1000);
    }, err => {
      console.log(err);
      this.notify.error(err.error.error.message, 'Update failed!');
    });
  }
}

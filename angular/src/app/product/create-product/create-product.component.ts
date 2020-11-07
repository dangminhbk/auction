import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
// import * as Editor from '@shared/ckeditor5/build/ckeditor';
import { CreateProductDto } from '../dto/create-product-dto';
import { MyUploadAdapter } from '@shared/ckeditor5/custom-adapter/custom-upload-adapter';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ImageService } from 'services/image/image.service';
import { BrandDto } from '@app/brand/dto/brand-dto';
import { BrandService } from 'services/brand/brand.service';
import { finalize, mergeMap, switchMap } from 'rxjs/operators';
import { forkJoin, from, Observable, of, zip } from 'rxjs';
import { ProductService } from 'services/product/product.service';
import { ImageListDto } from '../dto/image-list-dto';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ImagePickerComponent } from '@app/images/image-picker/image-picker.component';
import { ImageDto } from '@app/images/dto/image-dto';
import { Guid } from 'guid-typescript';
import { CurrencyMaskInputMode, NgxCurrencyModule } from "ngx-currency";
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent extends AppComponentBase {

  title: string;
  brands: BrandDto[] = [];
  images: ImageListDto[] = [];
  coverImage: ImageListDto;
  product: CreateProductDto = new CreateProductDto();
  public Editor = ClassicEditor;
  isEdit: false;
  moneyMode: CurrencyMaskInputMode.NATURAL;

  constructor(
    injector: Injector,
    private myUploadAdapter: MyUploadAdapter,
    private _brandService: BrandService,
    private _productService: ProductService,
    private _modalService: BsModalService,
    private _imageService: ImageService,
    private _router: Router,
  ) {
    super(injector);
    this.product.description = '';
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    abp.ui.setBusy();
    let apiCall: any;

    if (!this.isEdit) {
      apiCall = forkJoin(
        {
          brand: this._brandService.getDropdown()
        }
      );
    } else {
      apiCall = forkJoin(
        {
          product: this._productService.get(0),
          brand: this._brandService.getDropdown()
        }
      );
    }

    apiCall
      .pipe(finalize(() => {
        abp.ui.clearBusy();
      }))
      .subscribe(s => {
        this.brands = s.brand.result;

        if (this.isEdit) {
          this.product = s.product.result;
        }

      });
  }

  public onReady(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
      // Configure the URL to the upload script in your back-end here!

      const adapter = new MyUploadAdapter(this.injector.get(ImageService));
      adapter.loader = loader;
      return adapter;
    }
  }

  imageChange(event: any) {
    const files = event.target.files;
    for (let index = 0; index < files.length; index++) {
      const reader = new FileReader();
      reader.readAsDataURL(files[index]);
      reader.onload = () => {
        const image = new ImageListDto();
        image.id = Guid.create().toString();
        image.file = files[index];
        image.isUpload = true;
        image.url = reader.result.toString();
        this.images.push(image);
      }
    }

  }

  removeImage(id: string) {
    this.images = this.images.filter(s => s.id != id);
  }

  coverChange(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      const image = new ImageListDto();
      image.id = Guid.create().toString();
      image.file = file;
      image.isUpload = true;
      image.url = reader.result.toString();
      this.coverImage = image;
    }
  }

  openImagePicker(): void {
    const createDialog = this._modalService.show(ImagePickerComponent,
      {
        initialState: {
          pickMany: true,
          forAdmin: false
        }
      }
    );

    createDialog.content.onSavePickedImage.subscribe((s: ImageDto[]) => {
      if (s.length == 0) {
      } else {
        for (let index = 0; index < s.length; index++) {
          if (this.images.find(i => i.id == s[index].id.toString()) == undefined) {
            const image = new ImageListDto();
            image.id = s[index].id.toString();
            image.isUpload = false;
            image.url = this.getImagePath(s[index].url);
            this.images.push(image);
          }
        }
      }
    });
  }

  openCoverPicker(): void {
    const createDialog = this._modalService.show(ImagePickerComponent,
      {
        initialState: {
          pickMany: false,
          forAdmin: false
        }
      }
    );

    createDialog.content.onSavePickedImage.subscribe((s: ImageDto[]) => {
      if (s.length === 0) {
      } else {
        const image = new ImageListDto();
        image.id = s[0].id.toString();
        image.isUpload = false;
        image.url = this.getImagePath(s[0].url);
        this.coverImage = image;
      }
    });
  }

  save() {
    abp.ui.setBusy();
    const idImage = this.images.filter(s => !s.isUpload)
      .map(s => parseInt(s.id));

    this.product.imageIds = [];
    this.product.imageIds.push(...idImage);

    if (this.isEdit) {

    } else {
      // upload image
      let coverImageObserverble: Observable<any>;
      let uploadImageObserverble: Observable<any>;

      if (this.coverImage.isUpload) {
        const formData = new FormData();
        formData.append('Files', this.coverImage.file);
        coverImageObserverble = this._imageService.uploadSeller(formData);
      } else {
        coverImageObserverble = of({ result: [parseInt(this.coverImage.id)] });
      }

      const data = new FormData();
      const imageToUpload = this.images.filter(s => s.isUpload);
      if (imageToUpload.length > 0) {
        imageToUpload.forEach(s => {
          data.append('Files', s.file);
        });
        uploadImageObserverble = this._imageService.uploadSeller(data);
      } else {
        uploadImageObserverble = of({ result: [] });
      }

      zip(
        coverImageObserverble,
        uploadImageObserverble
      ).pipe(mergeMap(s => {
        this.product.imageIds.push(...s[1].result);
        this.product.coverImageId = s[0].result[0];
        console.log(this.product);
        return this._productService.create(this.product);
      }))
        .subscribe(s => {
          abp.notify.success('success');
          this._router.navigate(['/app/product']);
          abp.ui.clearBusy();
        }, s => {
          abp.notify.error('error');
          abp.ui.clearBusy();
        });

      // upload.pipe(mergeMap(s=> {
      //   this.product.imageIds.push(...s.result);
      //   return this._productService.create(this.product);
      // }), finalize(()=> {
      //   abp.ui.clearBusy();
      // }))
      // .subscribe(s=> {
      //   abp.notify.success('success');
      // },s=> {
      //   abp.notify.error('error');
      // });
      // create new product
    }
  }

}


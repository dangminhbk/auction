import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { ImageService } from 'services/image/image.service';
import { ImageDto } from '../dto/image-dto';

@Component({
  selector: 'app-image-picker',
  templateUrl: './image-picker.component.html',
  styleUrls: ['./image-picker.component.css'],
  animations: [appModuleAnimation()]
})
export class ImagePickerComponent extends PagedListingComponentBase<ImageDto> {

  @Input() pickMany = false;
  @Input() forAdmin = true;
  @Output() onSavePickedImage = new EventEmitter<any>();

  images: ImageDto[] = [];
  checkedImages: ImageDto[] = [];
  advancedFiltersVisible = false;
  isActive: boolean | null;
  constructor(
    private imageService: ImageService,
    public bsModalRef: BsModalRef,
    injector: Injector,
  ) {
    super(injector);
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {

    let serviceCall: Observable<any>;
    if (this.forAdmin) {
      serviceCall = this.imageService.getAll(request);       
    } else {
      serviceCall = this.imageService.getAllSeller(request);
    }

    serviceCall.pipe(
      finalize(() => {
        finishedCallback();
      })
    )
    .subscribe((result: ResultDto<ImageDto>) => {
      this.images = result.result.items;
      this.showPaging(result.result, pageNumber);
    });

  }
  protected delete(entity: any): void {
  }

  protected pick(): void {
    this.bsModalRef.hide();
  }

  isChecked(id: number) {
    const image = this.checkedImages.find(s => s.id === id);
    return image === null || image === undefined ? false : true;
  }

  onCheckChange(id: number, $event) {
    console.log(this.checkedImages);
    if (!$event.target.checked) {
      this.checkedImages = this.checkedImages.filter(s => s.id !== id);
      return;
    }
    if (this.pickMany || this.checkedImages.length == 0) {
      this.checkedImages.push(this.images.find(s => s.id === id));
      return;
    }
    if (this.checkedImages.length > 0) {
      $event.target.checked = false;
      abp.notify.info("Cannot pick more than one");
      return;
    }
  }

  save() {
    this.onSavePickedImage.emit(this.checkedImages);
    this.bsModalRef.hide();
  }

}

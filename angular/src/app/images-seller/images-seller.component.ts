import { Component, Injector, OnInit } from '@angular/core';
import { CreateImageComponent } from '@app/images/create-image/create-image.component';
import { ImageDto } from '@app/images/dto/image-dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { ImageService } from 'services/image/image.service';

@Component({
  selector: 'app-images-seller',
  templateUrl: './images-seller.component.html',
  styleUrls: ['./images-seller.component.css']
})
export class ImagesSellerComponent extends PagedListingComponentBase<ImageDto> {

  images: ImageDto[] = [];
  advancedFiltersVisible = false;
  isActive: boolean | null;
  constructor(
    private imageService: ImageService,
    injector: Injector,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.imageService.getAllSeller(request)
    .pipe(
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
    abp.message.confirm(
      'Do you want to delete?',
      undefined,
      (result: boolean) => {
        if (result) {
          this.imageService.deleteSeller(entity.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );

  }

  createImage()  {
    const createDialog = this._modalService.show(CreateImageComponent, { 
      initialState: {
        forAdmin: false
      }
    });

    createDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}

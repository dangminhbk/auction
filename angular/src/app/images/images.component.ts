import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ImageDto } from './dto/image-dto';
import { ImageService } from 'services/image/image.service';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { CreateImageComponent } from './create-image/create-image.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css'],
  animations: [appModuleAnimation()]
})

export class ImagesComponent extends PagedListingComponentBase<ImageDto> {

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

  createImage() {
    const createDialog = this._modalService.show(CreateImageComponent);

    createDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this.imageService.getAll(request)
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
          this.imageService.deleteSystem(entity.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );

  }

}

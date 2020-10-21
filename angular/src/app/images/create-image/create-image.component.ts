import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { ImageService } from 'services/image/image.service';
import { CreateImageDto } from '../dto/create-image-dto';

@Component({
  selector: 'app-create-image',
  templateUrl: './create-image.component.html',
  styleUrls: ['./create-image.component.css']
})
export class CreateImageComponent extends AppComponentBase implements OnInit {
  images: File[] = [];
  saving = false;
  @Output() onSave = new EventEmitter<any>();
  @Input() forAdmin = true;
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _imageService: ImageService
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

  save(): void {
    const form = new FormData;
    for (let index = 0; index < this.images.length; index++) {
      form.append('Files', this.images[index]);
    }

    let serviceCall = new Observable<any>();

    if (this.forAdmin) {
      serviceCall = this._imageService.uploadSystem(form);
    } else {
      serviceCall = this._imageService.uploadSeller(form);
    }

    serviceCall
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      ).subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      }, err => {
        console.log(err);
        this.notify.error(err.error.error.message, 'Upload failed!');
      });
  }

  handleFileSelect(files: FileList) {
    this.images = Array.from(files);
    console.log(this.images);
  }

}

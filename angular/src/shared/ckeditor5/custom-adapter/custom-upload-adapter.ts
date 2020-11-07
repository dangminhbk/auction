import { Injectable } from '@angular/core';
import { ImageService } from '../../../services/image/image.service';

@Injectable({
    providedIn: 'root'
})
export class MyUploadAdapter {
    private _loader: any;

    public set loader(v: any) {
        this._loader = v;
    }

    constructor(private _imageService: ImageService) {
    }

    // Starts the upload process.
    upload() {
        return this._loader.file
            .then(file => new Promise((resolve, reject) => {

                const formData = new FormData();
                formData.append('image', file);

                this._imageService
                    .uploadCK(formData)
                    .then(s => {
                        this._loader.uploaded = true;
                        resolve({ default: s.url });
                    })
            }));
    }


}

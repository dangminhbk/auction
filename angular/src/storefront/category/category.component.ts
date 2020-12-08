import { Component, Injector, OnInit } from '@angular/core';
import { CategoryDto } from '@app/category/Dto/category-dto';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { finalize } from 'rxjs/operators';
import { ResultDto } from 'services/base-api.service';
import { CategoryService } from 'services/category/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent extends PagedListingComponentBase<CategoryDto> implements OnInit {

  maxSize = 12;
  keyword = '';

  constructor(
    private _catService: CategoryService,
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.keyword = this.keyword;
    this._catService.getDropdown()
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: any) => {
        this.items = result.result;
        // this.showPaging(result.result, pageNumber);
      });
  }
  protected delete(entity: CategoryDto): void {
  }

}

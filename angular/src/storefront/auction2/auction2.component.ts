import { Component, Injector, OnInit } from '@angular/core';
import { AuctionComponent } from '../auction/auction.component';
import { AuctionService } from 'services/auction/auction.service';
import { BrandService } from 'services/brand/brand.service';
import { CategoryService } from 'services/category/category.service';
import { CurrencyPipe } from '@angular/common';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { BrandDto } from '@app/brand/dto/brand-dto';

@Component({
  selector: 'app-auction2',
  templateUrl: './auction2.component.html',
  styleUrls: ['./auction2.component.css']
})
export class Auction2Component extends AuctionComponent implements OnInit {

  title = '';
  subtitle = '';
  isBrand = false;
  brand: BrandDto = new BrandDto();
  constructor(
    _auctionService: AuctionService,
    _brandService: BrandService,
    _categoryService: CategoryService,
    currencyPipe: CurrencyPipe,
    injector: Injector,
    private _router: ActivatedRoute
  ) {
    super(
      _auctionService,
      _brandService,
      _categoryService,
      currencyPipe,
      injector);
  }
  ngOnInit(): void {
    this.isBrand = this._router.snapshot.data.isBrand || false;

    this.title = this._router.snapshot.data.text;

    if (this.isBrand) {
      this.brandId = parseInt(this._router.snapshot.paramMap.get('id'));
      this._brandService
      .getDropdown()
      .subscribe(s => {
        this.brands = [...s.result];
        this.brand = this.brands.find(s=>s.id == this.brandId);
        this.subtitle = this.brand.name;
      });

    } else {
      this.selectedCategories = [parseInt(this._router.snapshot.paramMap.get('id'))];
      this._categoryService
      .getDropdown()
      .subscribe(s => {
        this.categories = [...s.result];
        this.subtitle = this.categories.find(s=>s.id == this.selectedCategories[0]).name;
      });
    }





    this.getDataPage(this.pageNumber);
  }

}

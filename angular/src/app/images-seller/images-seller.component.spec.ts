import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImagesSellerComponent } from './images-seller.component';

describe('ImagesSellerComponent', () => {
  let component: ImagesSellerComponent;
  let fixture: ComponentFixture<ImagesSellerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImagesSellerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImagesSellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

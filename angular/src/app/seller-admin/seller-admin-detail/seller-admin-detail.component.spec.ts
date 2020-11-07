import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerAdminDetailComponent } from './seller-admin-detail.component';

describe('SellerAdminDetailComponent', () => {
  let component: SellerAdminDetailComponent;
  let fixture: ComponentFixture<SellerAdminDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerAdminDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerAdminDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

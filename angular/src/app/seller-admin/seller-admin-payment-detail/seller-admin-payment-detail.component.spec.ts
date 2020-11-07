import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerAdminPaymentDetailComponent } from './seller-admin-payment-detail.component';

describe('SellerAdminPaymentDetailComponent', () => {
  let component: SellerAdminPaymentDetailComponent;
  let fixture: ComponentFixture<SellerAdminPaymentDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerAdminPaymentDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerAdminPaymentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

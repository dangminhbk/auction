import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateInvoiceAddressComponent } from './update-invoice-address.component';

describe('UpdateInvoiceAddressComponent', () => {
  let component: UpdateInvoiceAddressComponent;
  let fixture: ComponentFixture<UpdateInvoiceAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateInvoiceAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateInvoiceAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

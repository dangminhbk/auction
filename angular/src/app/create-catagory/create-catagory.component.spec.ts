import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCatagoryComponent } from './create-catagory.component';

describe('CreateCatagoryComponent', () => {
  let component: CreateCatagoryComponent;
  let fixture: ComponentFixture<CreateCatagoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCatagoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCatagoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

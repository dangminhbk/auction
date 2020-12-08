import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticProductComponent } from './statistic-product.component';

describe('StatisticProductComponent', () => {
  let component: StatisticProductComponent;
  let fixture: ComponentFixture<StatisticProductComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatisticProductComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatisticProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

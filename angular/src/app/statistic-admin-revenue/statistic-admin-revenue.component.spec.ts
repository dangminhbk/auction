import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticAdminRevenueComponent } from './statistic-admin-revenue.component';

describe('StatisticAdminRevenueComponent', () => {
  let component: StatisticAdminRevenueComponent;
  let fixture: ComponentFixture<StatisticAdminRevenueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatisticAdminRevenueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatisticAdminRevenueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

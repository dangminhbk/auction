import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticOrderComponent } from './statistic-order.component';

describe('StatisticOrderComponent', () => {
  let component: StatisticOrderComponent;
  let fixture: ComponentFixture<StatisticOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatisticOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatisticOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

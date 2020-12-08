import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticAuctionComponent } from './statistic-auction.component';

describe('StatisticAuctionComponent', () => {
  let component: StatisticAuctionComponent;
  let fixture: ComponentFixture<StatisticAuctionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatisticAuctionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatisticAuctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

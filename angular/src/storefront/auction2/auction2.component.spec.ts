import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Auction2Component } from './auction2.component';

describe('Auction2Component', () => {
  let component: Auction2Component;
  let fixture: ComponentFixture<Auction2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Auction2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Auction2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

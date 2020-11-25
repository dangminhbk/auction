import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import * as dayjs from 'dayjs';
import { CountdownConfig } from 'ngx-countdown';

@Component({
  selector: 'app-auction-countdown',
  templateUrl: './auction-countdown.component.html',
  styleUrls: ['./auction-countdown.component.css']
})
export class AuctionCountdownComponent extends AppComponentBase implements OnInit {

  @Input() endTime;
  @Output() onEndCountdown = new EventEmitter<any>();
  timeConfig: CountdownConfig;

  constructor(
    injector: Injector
  ) {
    super(injector);
   }

  ngOnInit(): void {
    this.timeConfig = { leftTime: this.calculateEndTime(this.endTime), format: 'HH:mm:ss', demand: false };
  }

  calculateEndTime(date: Date) {
    return dayjs(date + 'z').unix() - dayjs(new Date()).unix();
  }

  handleEvent(event: any) {
    console.log(event);
    if ( event.action === 'done') {
      this.onEndCountdown.emit(null);
    }
  }
}

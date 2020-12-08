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
    this.timeConfig = {
      leftTime: this.calculateEndTime(this.endTime), formatDate: ({ date, formatStr }) => {
        let duration = Number(date || 0);

        return CountdownTimeUnits.reduce((current, [name, unit]) => {
          if (current.indexOf(name) !== -1) {
            const v = Math.floor(duration / unit);
            duration -= v * unit;
            return current.replace(new RegExp(`${name}+`, 'g'), (match: string) => {
              return v.toString().padStart(match.length, '0');
            });
          }
          return current;
        }, formatStr);
      }, demand: false
    };
  }


  calculateEndTime(date: Date) {
    return dayjs(date + 'z').unix() - dayjs(new Date()).unix();
  }

  handleEvent(event: any) {
    console.log(event);
    if (event.action === 'done') {
      this.onEndCountdown.emit(null);
    }
  }
}

const CountdownTimeUnits: Array<[string, number]> = [
  ['Y', 1000 * 60 * 60 * 24 * 365], // years
  ['M', 1000 * 60 * 60 * 24 * 30], // months
  ['D', 1000 * 60 * 60 * 24], // days
  ['H', 1000 * 60 * 60], // hours
  ['m', 1000 * 60], // minutes
  ['s', 1000], // seconds
  ['S', 1], // million seconds
];

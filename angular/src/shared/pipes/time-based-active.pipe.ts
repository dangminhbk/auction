import { Pipe, PipeTransform } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Pipe({
  name: 'timeBasedActive'
})
export class TimeBasedActivePipe extends AppComponentBase implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): string {
    console.log(value);
    const date = new Date();
    return (value as Date) > date ? 'Active' : 'Expired';
  }

}

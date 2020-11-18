import { TimeBasedActivePipe } from './time-based-active.pipe';

describe('TimeBasedActivePipe', () => {
  it('create an instance', () => {
    const pipe = new TimeBasedActivePipe();
    expect(pipe).toBeTruthy();
  });
});

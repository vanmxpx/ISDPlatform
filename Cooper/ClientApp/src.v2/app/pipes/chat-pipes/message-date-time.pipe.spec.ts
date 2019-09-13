import { MessageDateTimePipe } from './message-date-time.pipe';

describe('MessageDateTimePipe', () => {
  it('create an instance', () => {
    const pipe = new MessageDateTimePipe();
    expect(pipe).toBeTruthy();
  });
});

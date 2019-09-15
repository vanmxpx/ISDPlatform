import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'messageDateTime'
})
export class MessageDateTimePipe implements PipeTransform {

  public monthNames: string[] = [
    'Jan', 'Feb', 'Mar',
    'Apr', 'May', 'Jun', 'Jul',
    'Aug', 'Sep', 'Oct',
    'Nov', 'Dec'
  ];

  public dayNames: string[] = [
    'Mon', 'Tue', 'Wed',
    'Thu', 'Fri', 'Sat', 'Sun'
  ];

  public transform(value: Date): string {
    const date: Date = new Date(Date.parse(value.toString()));
    const dateNow: Date = new Date();
    let messageDateTime: string;

    if (dateNow.getFullYear() !== date.getFullYear()) {
      const year: string = (date.getFullYear() % 100).toString();
      const month: string = date.getMonth().toString();
      const dayOfMonth: string = date.getDate().toString();

      messageDateTime = `${dayOfMonth}.${month}.${year}`;

    } else if (dateNow.getMonth() !== date.getMonth() ||
                dateNow.getDate() - date.getDate() > 6) {

      const month: string = this.monthNames[date.getMonth()];
      const dayOfMonth: string = date.getDate().toString();

      messageDateTime = `${month} ${dayOfMonth}`;

    } else if (dateNow.getDay() !== date.getDay()) {

      const dayOfWeek: string = this.dayNames[date.getDay() - 1];

      messageDateTime = `${dayOfWeek}`;

    } else {
      let hours: string = date.getHours().toString();
      let minutes: string = date.getMinutes().toString();

      if (hours.length === 1) {
        hours = '0' + hours;
      }

      if (minutes.length === 1) {
        minutes = '0' + minutes;
      }

      messageDateTime = `${hours}:${minutes}`;
    }

    return messageDateTime;
  }

}

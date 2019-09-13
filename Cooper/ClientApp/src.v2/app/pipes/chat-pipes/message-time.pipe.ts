import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'messageTime'
})
export class MessageTimePipe implements PipeTransform {

  public transform(value: Date): string {
    const date: Date = new Date(Date.parse(value.toString()));

    const hours: number = (date.getHours() === 12) ? date.getHours() : date.getHours() % 12;
    let minutes: string = date.getMinutes().toString();
    let seconds: string = date.getSeconds().toString();

    const partOfDay: string = (date.getHours() < 13) ? 'AM' : 'PM';

    if (minutes.length === 1) {
      minutes = '0' + minutes;
    }

    if (seconds.length === 1) {
      seconds = '0' + seconds;
    }

    const result: string = `${hours}:${minutes}:${seconds} ${partOfDay}`;

    return result;
  }

}

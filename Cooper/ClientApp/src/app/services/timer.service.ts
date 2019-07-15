import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimerService {
  
  private formatStringMinutes(number: number): string {
    if(number == 1) return "a minute ago"
    else return number + " minutes ago"
  }
  
  private formatStringHours(number: number): string {
    if(number == 1) return "an hour ago"
    else return number + " hours ago"
  }
  
  timeAgo(createDate: number): string {
    var date = Date.now();
    var definition = date-createDate*1000;
    if(definition< 60000) return "recently"
    if(definition< 3600000) return this.formatStringMinutes(Math.round(definition/60000))
    if(definition< 86400000) return this.formatStringHours(Math.round(definition/86400000))
  }
  
  constructor() { }
}

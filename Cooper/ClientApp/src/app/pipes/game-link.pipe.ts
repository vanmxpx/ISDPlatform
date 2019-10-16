import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'gameLink'
})
export class GameLinkPipe implements PipeTransform {

  public transform(link: string): string {
    return link.replace('proxy/', '');
  }

}

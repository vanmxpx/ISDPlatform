import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'messageContent' })

export class MessageContentPipe implements PipeTransform {
  public transform(messageContent: string): string {
      const result = messageContent.replace('\n', '');

      return result;
  }
}

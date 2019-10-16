import { Pipe, PipeTransform } from '@angular/core';
import { LanguagesHelper } from '@helpers';

@Pipe({
  name: 'languageName'
})
export class LanguageNamePipe implements PipeTransform {
  public helper: any = LanguagesHelper;
  public transform(value: any): any {
    return this.helper.getLanguageTitle(value);
  }

}

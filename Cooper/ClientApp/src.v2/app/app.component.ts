import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from '@services';

@Component({
  selector: 'coop-root',
 templateUrl: './app.component.html',
 styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public translate: TranslateService, private localizationService: LocalizationService) {
    localizationService.switchLanguage();
  }
}





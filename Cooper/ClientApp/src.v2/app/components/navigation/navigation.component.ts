import { Component, Input } from '@angular/core';
import {CoopNavBarItem} from '@models';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'coop-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {

  @Input() public items: CoopNavBarItem[];
  constructor(public translate: TranslateService) { }

}

import { Component, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatInputModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-root',
 templateUrl: './app.component.html',
 styleUrls: ['./app.component.css']
})
export class AppComponent {
  userName: string = "";
  password: string = "";
  response: any;

  constructor(private http : HttpClient){
  }
}

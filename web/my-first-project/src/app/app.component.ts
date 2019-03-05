import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
 templateUrl: './app.component.html',
 styleUrls: ['./app.component.css'],
  
})
export class AppComponent {
  userName: string = "";
  password: string = "";
  response: any;

  constructor(private http : HttpClient){
  }
  search(){
    this.http.get('https://localhost:5001/api/values'/*+this.userName*/).subscribe((response)=>{
      this.response=response;
      console.log(this.response);
    })
  }
}

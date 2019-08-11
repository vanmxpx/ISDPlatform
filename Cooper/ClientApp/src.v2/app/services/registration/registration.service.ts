import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

const registrationUrl = '/registration';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {


  constructor(private router: Router, private http: HttpClient) { }

  public register(body: any) {

    this.http.post(registrationUrl, body).subscribe(
      (res: any) => {
        this.router.navigate(['/login']);
        },
      err => {
        console.log(err);
      }
    );
    }


}

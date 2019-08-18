import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

const registrationUrl = '/registration';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private router: Router, private http: HttpClient) { }

  public register(body: any): void {

    this.http.post(registrationUrl, body).subscribe(
      () => {
        this.router.navigate(['/login']);
        },
      (err) => {
        console.log(err);
      }
    );
    }

}

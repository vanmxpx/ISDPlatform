import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  readonly registrationUrl = '/registration';

  constructor(private router: Router, private http: HttpClient) { }

  public register(body) {

    this.http.post(this.registrationUrl, body).subscribe(
      (res: any) => {
        this.router.navigate(['/myPage', 'my']);
        },
      err => {
        console.log(err);
      }
    );
    }


}

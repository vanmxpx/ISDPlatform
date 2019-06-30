import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { FormControl,NgForm, FormGroupDirective, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from "@angular/router";
import { UserService } from '../../../services/user.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
  
  constructor(private http: HttpClient, private router: Router,private service: UserService) {
    this.CheckAuthentification();
  }
  public user: any;
  
  CheckAuthentification(): void {
    const Token: string = localStorage.getItem('JwtCooper');
    if (Token) {
      this.router.navigate(['/myPage', "my"]);
    }
  }
  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        this.router.navigate(['/myPage', "my"]);
        },
      err => {
        console.log(err);
      }
    );
  }

  socialSignIn(platform) {
    this.service.socialSignIn(platform);
  }

 ngOnInit() { 
    this.service.formModel.reset();
  }
}

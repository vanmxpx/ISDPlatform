import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { FormControl, NgForm, FormGroupDirective, FormBuilder, FormGroup, Validators } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  registerForm: FormGroup;
    loading = false;
    submitted = false;



  constructor(private router: Router) {
    this.CheckAuthentification();
   }

  CheckAuthentification(): void {
    const Token: string = localStorage.getItem('JwtCooper');
    if (Token) {
      this.router.navigate(['/home']);
    }
  }

  ngOnInit() {}
}

import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { FormControl,NgForm, FormGroupDirective, FormBuilder, FormGroup, Validators } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  registerForm: FormGroup;
    loading = false;
    submitted = false;
  constructor(){
      
   }
  ngOnInit() {}
}

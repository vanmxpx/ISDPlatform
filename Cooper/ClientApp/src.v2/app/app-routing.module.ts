import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginLayoutComponent} from './layouts';

const routes: Routes = [
{path: 'login', component: LoginLayoutComponent}
];

@NgModule({
  imports:
    [
      RouterModule.forRoot(routes)
    ],
  exports:
    [
      RouterModule
    ],
  declarations: []
})
export class AppRoutingModule { }

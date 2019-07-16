import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent} from './layouts';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginLayoutComponent},
  {path: 'registration', component: RegistrationLayoutComponent},
  {path: 'platform', component: PlatformLayoutComponent},
  {path: '**', component: PageNotFoundLayoutComponent}
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

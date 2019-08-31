import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from '@guards';
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent, GameLayoutComponent, GamesLayoutComponent, ProfileLayoutComponent,
  HomeLayoutComponent, ResetPasswordLayoutComponent, ConfirmPasswordLayoutComponent} from '@layouts';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginLayoutComponent},
  {path: 'reset', component: ResetPasswordLayoutComponent},
  {path: 'confirm', component: ConfirmPasswordLayoutComponent},
  {path: 'registration', component: RegistrationLayoutComponent},
  {path: 'platform', component: PlatformLayoutComponent, canActivate: [AuthGuard],
   children:  [
    {path: 'games', component: GamesLayoutComponent},
    {path: 'games/:link', component: GameLayoutComponent},
    {path: 'profile/:nickname', component: ProfileLayoutComponent},
    {path: 'home', component: HomeLayoutComponent},
  ]},
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

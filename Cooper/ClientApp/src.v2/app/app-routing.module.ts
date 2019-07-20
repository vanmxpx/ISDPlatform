import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from '@guards';
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent, GameLayoutComponent, GamesLayoutComponent, ProfileLayoutComponent} from '@layouts';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginLayoutComponent},
  {path: 'registration', component: RegistrationLayoutComponent},
  {path: 'platform', component: PlatformLayoutComponent, canActivate: [AuthGuard],
   children:  [
    {path: 'games', component: GamesLayoutComponent},
    {path: 'game/:id', component: GameLayoutComponent},
    {path: 'profile/:nickname', component: ProfileLayoutComponent}
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

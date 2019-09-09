import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
<<<<<<< HEAD
=======
import {AuthGuard, ExitGuard} from '@guards';
>>>>>>> ddc2e015f7d82792ebd3ea8508459b9ae553f430
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent, GameLayoutComponent, GamesLayoutComponent, ProfileLayoutComponent, HomeLayoutComponent} from '@layouts';
import { AuthGuard } from '@guards';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginLayoutComponent},
  {path: 'registration', component: RegistrationLayoutComponent},
  {path: 'platform', component: PlatformLayoutComponent, canActivate: [AuthGuard],
   children:  [
    {path: 'games', component: GamesLayoutComponent},
    {path: 'games/:link', component: GameLayoutComponent, canDeactivate: [ExitGuard]},
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

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard, ExitGuard} from '@guards';
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent, GameLayoutComponent, GamesLayoutComponent, ProfileLayoutComponent,

  HomeLayoutComponent, ResetPasswordLayoutComponent, ConfirmPasswordLayoutComponent,
  PersonalChatsLayoutComponent, SettingsLayoutComponent, GameLoadErrorLayoutComponent} from '@layouts';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginLayoutComponent},
  {path: 'reset', component: ResetPasswordLayoutComponent},
  {path: 'confirm', component: ConfirmPasswordLayoutComponent},
  {path: 'registration', component: RegistrationLayoutComponent},
  {path: 'platform', component: PlatformLayoutComponent, canActivate: [AuthGuard],
   children:  [
    {path: 'games', component: GamesLayoutComponent},
    {path: 'games/:link', component: GameLayoutComponent, canDeactivate: [ExitGuard]},
    {path: 'profile/:nickname', component: ProfileLayoutComponent},
    {path: 'settings', component: SettingsLayoutComponent},
    {path: 'home', component: HomeLayoutComponent},
    {path: 'chats', component: PersonalChatsLayoutComponent}
  ]},
  {path: 'proxy/:link', component: GameLoadErrorLayoutComponent},
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

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginLayoutComponent, RegistrationLayoutComponent, PlatformLayoutComponent,
  PageNotFoundLayoutComponent, GameLayoutComponent, GamesLayoutComponent, ProfileLayoutComponent, EntryLayoutComponent} from './layouts';

const routes: Routes = [
  {path: '', redirectTo: 'entry/login', pathMatch: 'full'},
  {path: 'entry', component: EntryLayoutComponent,
   children: [
    {path: 'login', component: LoginLayoutComponent},
    {path: 'registration', component: RegistrationLayoutComponent},
   ]},
  {path: 'platform', component: PlatformLayoutComponent,
   children:  [
    {path: 'games', component: GamesLayoutComponent},
    {path: 'game:id', component: GameLayoutComponent},
    {path: 'profile:nickname', component: ProfileLayoutComponent}
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

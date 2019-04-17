import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GameComponent } from './game/game.component';
import { AppSignComponentComponent } from './app-sign-component/app-sign-component.component';
import { AuthGuard } from './guards/auth-guard';
import { MyPageComponent } from './my-page/my-page.component';

const routes: Routes = [
  {path: '', redirectTo : 'auth', pathMatch: 'full'},
  { path: 'home', component: HomeComponent },
  { path: 'game', component: GameComponent },
  { path: 'myPage', component: MyPageComponent },
  { path: 'auth', component: AppSignComponentComponent,
    children: [
      {path: '', component: SignInComponent, outlet:'sub', data: { animation: 'isSignIn' }},
      {path:'signUp',component:SignUpComponent, outlet:'sub', data: { animation: 'isSignUp' }},
      ]},
     
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

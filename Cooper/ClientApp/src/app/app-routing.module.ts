import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { AppComponent } from './app.component';
import { GamesComponent } from './components/games/games.component';
import { GameDetailComponent }  from './components/game-detail/game-detail.component';
import { HomeComponent } from './components/home/home.component';
import { GameComponent } from './components/home/game/game.component';
import { AuthGuard } from './components/auth/guards/auth-guard';
import { AppSignComponentComponent } from './app-sign-component/app-sign-component.component';
import { MyPageComponent } from './my-page/my-page.component';

const routes: Routes = [
  
  { path: '', redirectTo : 'auth', pathMatch: 'full'},
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'game', component: GameComponent, canActivate: [AuthGuard] },
  { path: 'gamedetail/:id', component: GameDetailComponent, canActivate: [AuthGuard] },
  { path: 'games', component: GamesComponent, canActivate: [AuthGuard] },
  { path: 'myPage/:nickname', component: MyPageComponent, canActivate: [AuthGuard]},
  { path: '**', redirectTo : 'auth/signIn', pathMatch: 'full'},
  { path: 'auth', component: AppSignComponentComponent,
    children: [
      { path: '', component: SignInComponent, outlet:'sub', data: { animation: 'isSignIn' }},
      { path:'signUp',component:SignUpComponent, outlet:'sub', data: { animation: 'isSignUp' }},
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

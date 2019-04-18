import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { GameComponent } from './components/home/game/game.component';
import { GamesComponent } from './components/games/games.component';
import { GameDetailComponent }  from './components/game-detail/game-detail.component';

import { AuthGuard } from './components/auth/guards/auth-guard';


const routes: Routes = [
  { path: '', redirectTo: '/signIn', pathMatch: 'full' },
  { path: 'signIn', component: SignInComponent, data: { animation: 'isSignIn' } },
  { path: 'signUp', component: SignUpComponent, data: { animation: 'isSignUp' } },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'game', component: GameComponent, canActivate: [AuthGuard] },
  { path: 'gamedetail/:id', component: GameDetailComponent, canActivate: [AuthGuard] },
  { path: 'games', component: GamesComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/signIn' }       // while not found page is not implemented
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

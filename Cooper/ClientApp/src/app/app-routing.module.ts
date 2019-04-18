import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GameComponent } from './home/game/game.component';
import { GamesComponent } from './components/games/games.component';
import { GameDetailComponent }  from './components/game-detail/game-detail.component';

import { AuthGuard } from './components/auth/guards/auth-guard';


const routes: Routes = [
  { path: '', redirectTo: '/signIn', pathMatch: 'full' },
  { path: 'signIn', component: SignInComponent, data: { animation: 'isSignIn' } },
  { path: 'signUp', component: SignUpComponent, data: { animation: 'isSignUp' } },
  { path: 'home', component: HomeComponent },
  { path: 'game', component: GameComponent },
  { path: 'gamedetail/:id', component: GameDetailComponent },
  { path: 'games', component: GamesComponent }

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

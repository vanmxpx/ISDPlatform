import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { AuthGuard } from './guards/auth-guard';

import { HttpClient } from 'selenium-webdriver/http';
import { FirstServiceService } from './first-service.service';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule, MatButtonModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTableModule} from '@angular/material/table';
import {trigger,transition, style, query,group,animateChild, animate, keyframes,} from '@angular/animations';
import { GameComponent } from './game/game.component';

import { GamesComponent } from './components/games/games.component';
import { GameDetailComponent } from './components/game-detail/game-detail.component';
import { GameSearchComponent } from './components/game-search/game-search.component';
import { SafePipe } from './components/safepipe';

const appRoutes : Routes = [
  {path: '', redirectTo : '/signIn', pathMatch: 'full'},
  {path: 'signIn', component: SignInComponent, data: { animation: 'isSignIn' }},
  {path: 'signUp', component: SignUpComponent, data: { animation: 'isSignUp' }}
]

@NgModule({
  declarations: [
    NavMenuComponent,
    AppComponent,
    SignInComponent,
    SignUpComponent,
    HomeComponent,
    GameComponent,
    GamesComponent,
    GameDetailComponent,
    GameSearchComponent,
    SafePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule, 
    MatInputModule,
    MatFormFieldModule,
    MatTableModule,
    MatButtonModule 
  ],
  providers: [FirstServiceService],
  bootstrap: [AppComponent,
  SignInComponent,
SignUpComponent]
})
export class AppModule { }
export class PizzaPartyAppModule { }
export class InputOverviewExample {}

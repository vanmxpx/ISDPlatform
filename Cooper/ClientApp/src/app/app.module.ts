import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './components/auth/guards/auth-guard';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule, MatButtonModule, MatCardModule, MatListModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTableModule} from '@angular/material/table';
import { AppSignComponentComponent } from './app-sign-component/app-sign-component.component';
import { MyPageComponent } from './my-page/my-page.component';
import { GameComponent } from './components/home/game/game.component';
import { GamesComponent } from './components/games/games.component';
import { GameDetailComponent } from './components/game-detail/game-detail.component';
import { GameSearchComponent } from './components/game-search/game-search.component';
import { SafePipe } from './pipes/safe.pipe';

const appRoutes : Routes = [
  {path: '', redirectTo : '/signIn', pathMatch: 'full'},
  {path: 'signIn', component: SignInComponent, data: { animation: 'isSignIn' }},
  {path: 'signUp', component: SignUpComponent, data: { animation: 'isSignUp' }}
]

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    SignUpComponent,
    HomeComponent,
    GameComponent,
    AppSignComponentComponent,
    MyPageComponent,
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
    MatButtonModule,
    MatCardModule,
    MatListModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent,
  SignInComponent,
  SignUpComponent]
})
export class AppModule { }
export class PizzaPartyAppModule { }
export class InputOverviewExample {}

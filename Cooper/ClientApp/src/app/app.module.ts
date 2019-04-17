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
import { FirstServiceService } from './first-service.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule, MatButtonModule, MatCardModule, MatListModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTableModule} from '@angular/material/table';
import { GameComponent } from './game/game.component';
import { AppSignComponentComponent } from './app-sign-component/app-sign-component.component';
import { MyPageComponent } from './my-page/my-page.component';

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
    AppSignComponentComponent,
    MyPageComponent
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
  providers: [FirstServiceService],
  bootstrap: [AppComponent,
  SignInComponent,
SignUpComponent]
})
export class AppModule { }
export class PizzaPartyAppModule { }
export class InputOverviewExample {}

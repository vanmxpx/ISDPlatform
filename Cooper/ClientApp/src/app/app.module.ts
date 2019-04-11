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
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { AuthGuard } from './guards/auth-guard';

import { HttpClient } from 'selenium-webdriver/http';
import { FirstServiceService } from './first-service.service';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule, MatButtonModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTableModule} from '@angular/material/table';
import { GameComponent } from './game/game.component';


@NgModule({
  declarations: [
    NavMenuComponent,
    AppComponent,
    SignInComponent,
    SignUpComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    GameComponent,
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

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
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
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from './services/user.service';
import {DynamiSocialLoginModule,AuthServiceConfig, GoogleLoginProvider,FacebookLoginProvider} from 'ng-dynami-social-login';
import { CooperInterceptor } from 'src/assets/cooper.interceptor';
import { GrowlModule } from 'primeng/primeng';
import {MatIconModule} from '@angular/material/icon';
import { ChatsComponent } from './components/chats/chats.component';
import { ChatDetailComponent } from './components/chat-detail/chat-detail.component';
import { MessagesComponent } from './components/messages/messages.component';


const appRoutes : Routes = [
  {path: '', redirectTo : '/signIn', pathMatch: 'full'},
  {path: 'signIn', component: SignInComponent, data: { animation: 'isSignIn' }},
  {path: 'signUp', component: SignUpComponent, data: { animation: 'isSignUp' }}
]
export function getAuthServiceConfigs() {
  let config = new AuthServiceConfig(
      [
         {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider("984923305039531")
        },
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider("404859751108-vfr28h3l9dju1fovr0j4m99dn3flig22.apps.googleusercontent.com")
        }
         
      ]
  );
  return config;
}
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
    ChatsComponent,
    ChatDetailComponent,
    MessagesComponent,
    SafePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, 
    MatInputModule,
    MatFormFieldModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule,
    MatListModule,
    DynamiSocialLoginModule,
    GrowlModule,
    MatIconModule
  ],
  providers: [
    {
    provide: AuthServiceConfig,
    useFactory: getAuthServiceConfigs
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CooperInterceptor,
      multi: true // give the possibility of various interceptors
    }, 
    AuthGuard, 
    UserService
],
  bootstrap: [AppComponent,
]
})
export class AppModule { }
export class PizzaPartyAppModule { }
export class InputOverviewExample {}


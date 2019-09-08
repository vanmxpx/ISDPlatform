import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { DynamiSocialLoginModule, AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from 'ng-dynami-social-login';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatInputModule, MatButtonModule, MatCardModule, MatListModule, MatTabsModule,
  MatBadgeModule, MatGridListModule, MatRippleModule} from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTableModule} from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';
import { CooperInterceptor } from '@services';
import { GrowlModule } from 'primeng/primeng';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { AuthGuard, ExitGuard } from '@guards';
import { SafePipe } from '@pipes';
import { LoginLayoutComponent, GameLayoutComponent, GamesLayoutComponent,
  PlatformLayoutComponent, ProfileLayoutComponent, TopPanelLayoutComponent,
  RegistrationLayoutComponent, PageNotFoundLayoutComponent, UploadLayoutComponent, HomeLayoutComponent,
  PersonalChatsLayoutComponent } from '@layouts';

import {LoginFormComponent, RegistrationFormComponent, PosterComponent, UserConnectionsListComponent,
  GamesListComponent, GameListItemComponent, UploadComponent, UserInfoComponent, MyProfileComponent, GameCardComponent,
   NavigationComponent, AvatarCardComponent, ChatModalWindowComponent, ChatsListComponent, ChatBoxComponent,
    MessagePanelComponent} from '@components';
import { MatDialogModule } from '@angular/material';

export function getAuthServiceConfigs(): AuthServiceConfig {
  const config = new AuthServiceConfig(
      [
         {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider('INSERT_FACEBOOK_APP_ID')
        },
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider('INSERT_GOOGLE_APP_ID')
        }

      ]
  );
  return config;
}
@NgModule({
  declarations: [
    AppComponent,
    LoginLayoutComponent,
    RegistrationLayoutComponent,
    GameLayoutComponent,
    GamesLayoutComponent,
    TopPanelLayoutComponent,
    ProfileLayoutComponent,
    PlatformLayoutComponent,
    PageNotFoundLayoutComponent,
    HomeLayoutComponent,
    PersonalChatsLayoutComponent,
    SafePipe,

    LoginFormComponent,
    RegistrationFormComponent,
    PosterComponent,
    UserConnectionsListComponent,
    GamesListComponent,
    UserInfoComponent,
    MyProfileComponent,
    GameListItemComponent,
    GameCardComponent,
    NavigationComponent,
    AvatarCardComponent,
  ChatModalWindowComponent,
  ChatsListComponent,
  ChatBoxComponent,
  MessagePanelComponent,
  UploadComponent,
  UploadLayoutComponent
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
    MatDialogModule,
    DynamiSocialLoginModule,
    GrowlModule,
    MatIconModule,
    MatTabsModule,
    MatBadgeModule,
    MatGridListModule,
    MatRippleModule,
    MatProgressBarModule,
    MatButtonToggleModule
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
    ExitGuard,

  ],
  entryComponents: [UploadLayoutComponent],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
export class PizzaPartyAppModule { }
export class InputOverviewExample { }

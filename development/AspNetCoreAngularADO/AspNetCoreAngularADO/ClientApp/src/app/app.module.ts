import { NgModule } from '@angular/core';
import { GameService } from './services/gameservice.service'
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchGameComponent } from './components/fetchgame/fetchgame.component'
import { creategame } from './components/addgame/AddGame.component'
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchGameComponent,
    creategame,
  ],
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'fetch-game', component: FetchGameComponent },
      { path: 'register-game', component: creategame },
      { path: 'game/edit/:id', component: creategame },
      { path: '**', redirectTo: 'home' }
    ])
  ],
  providers: [GameService]
})
export class AppModuleShared {
}


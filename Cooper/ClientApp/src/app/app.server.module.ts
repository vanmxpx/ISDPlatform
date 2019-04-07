import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
    imports: [AppModule, ServerModule, ModuleMapLoaderModule, BrowserAnimationsModule],
    bootstrap: [AppComponent],
    
})
export class AppServerModule { }
export class PizzaPartyAppModule { }

import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppStoreModule } from './store/app-store.module';
import { RootStoreFacade } from './store/facade';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppStoreModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule
  ],
  providers: [RootStoreFacade],
  bootstrap: [AppComponent]
})
export class AppModule { }

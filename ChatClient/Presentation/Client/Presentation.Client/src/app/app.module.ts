import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from '@chat-client/auth';
import { RootStoreModule } from './store/root-store.module';
import { SplashScreenModule } from './shared/splash-screen/splash-screen.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    RootStoreModule,
    BrowserModule,
    HttpClientModule,
    AuthModule,
    SplashScreenModule,
    BrowserAnimationsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

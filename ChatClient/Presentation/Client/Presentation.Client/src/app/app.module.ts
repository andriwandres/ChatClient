import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule } from '@chat-client/shared/auth';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LanguageModule } from './shared/language';
import { SplashScreenModule } from './shared/splash-screen/splash-screen.module';
import { TranslationModule } from './shared/translation';
import { RootStoreModule } from './store/root-store.module';

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
    BrowserAnimationsModule,
    LanguageModule,
    MatSnackBarModule,
    TranslationModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

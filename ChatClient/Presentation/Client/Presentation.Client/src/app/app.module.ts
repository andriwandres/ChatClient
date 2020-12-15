import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from '@chat-client/auth';
import { RootStoreModule } from './store/root-store.module';
import { SplashScreenModule } from './shared/splash-screen/splash-screen.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Translation } from './core/models/translation';
import { LanguageModule } from './shared/language';

export function getTranslateLoader(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, `${environment.api.languages}/`)
}

export class CustomLoader implements TranslateLoader {
  constructor(private readonly http: HttpClient) {}

  getTranslation(lang: string): Observable<Translation[]> {
    const url = `${environment.api.languages}/${lang}/translations`;

    return this.http.get<Translation[]>(url);
  }
}

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
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: CustomLoader,
        deps: [HttpClient]
      },
    })
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

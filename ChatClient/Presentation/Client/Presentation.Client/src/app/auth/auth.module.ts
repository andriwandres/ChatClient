import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthInterceptor } from '@chat-client/core/interceptors';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthStoreModule } from './store';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthRoutingModule,
    AuthStoreModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class AuthModule { }

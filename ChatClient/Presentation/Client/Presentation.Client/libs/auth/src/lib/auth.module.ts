import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthStoreModule } from './auth-store.module';

@NgModule({
  imports: [
    AuthRoutingModule,
    AuthStoreModule,
    CommonModule,
  ],
})
export class AuthModule {}

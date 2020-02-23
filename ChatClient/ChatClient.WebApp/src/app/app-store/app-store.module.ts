import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { AuthStoreModule } from './auth-store/auth-store.module';
import { LatestMessagesStoreModule } from './latest-messages-store/latest-messages-store.module';
import { ChatMessagesStoreModule } from './chat-messages-store/chat-messages-store.module';

@NgModule({
  imports: [
    AuthStoreModule,
    LatestMessagesStoreModule,
    ChatMessagesStoreModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
  ]
})
export class AppStoreModule { }

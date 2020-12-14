import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { SignalREffects, signalrReducer } from 'ngrx-signalr-core';
import { MessengerEffects } from './effects';
import { MESSENGER_FEATURE_KEY } from './state';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature(MESSENGER_FEATURE_KEY, { signalR: signalrReducer }),
    EffectsModule.forFeature([SignalREffects, MessengerEffects])
  ],
  providers: []
})
export class MessengerStoreModule { }

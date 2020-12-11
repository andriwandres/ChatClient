import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { SignalREffects, signalrReducer } from 'ngrx-signalr-core';
import { MESSENGER_FEATURE_KEY } from './state';
import { EffectsModule } from '@ngrx/effects';
import { MessengerEffects } from './effects';
import { MessengerFacade } from './facade';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature(MESSENGER_FEATURE_KEY, { signalR: signalrReducer }),
    EffectsModule.forFeature([SignalREffects, MessengerEffects])
  ],
  providers: [MessengerFacade]
})
export class MessengerStoreModule { }

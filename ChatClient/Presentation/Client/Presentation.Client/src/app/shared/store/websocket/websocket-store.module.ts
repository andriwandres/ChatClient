import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { SignalREffects, signalrReducer } from 'ngrx-signalr-core';
import { WebSocketEffects } from './websocket.effects';
import { SOCKET_FEATURE_KEY } from './websocket.state';

@NgModule({
  declarations: [],
  imports: [
    StoreModule.forFeature(SOCKET_FEATURE_KEY, { signalR: signalrReducer }),
    EffectsModule.forFeature([SignalREffects, WebSocketEffects])
  ],
  providers: []
})
export class WebSocketStoreModule { }

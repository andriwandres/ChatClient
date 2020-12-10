import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { SignalREffects, signalrReducer } from 'ngrx-signalr-core';
import { environment } from 'src/environments/environment';
import { RootStoreEffects } from './effects';
import { RootStoreFacade } from './facade';

@NgModule({
  declarations: [],
  imports: [
    StoreModule.forRoot({ signalR: signalrReducer }),
    EffectsModule.forRoot([SignalREffects, RootStoreEffects]),
    environment.production ? [] : StoreDevtoolsModule.instrument({
      maxAge: 25,
    }),
  ],
  providers: [RootStoreFacade]
})
export class RootStoreModule { }

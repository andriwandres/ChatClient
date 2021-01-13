import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { MessageEffects } from './effects';
import { messagesReducer } from './reducer';
import { MESSAGES_FEATURE_KEY } from './state';

@NgModule({
  imports: [
    StoreModule.forFeature(MESSAGES_FEATURE_KEY, messagesReducer),
    EffectsModule.forFeature([MessageEffects])
  ]
})
export class MessagesStoreModule {}

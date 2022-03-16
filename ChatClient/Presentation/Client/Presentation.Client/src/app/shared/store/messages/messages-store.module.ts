import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { MessageEffects } from './messages.effects';
import { messagesReducer } from './messages.reducer';
import { MESSAGES_FEATURE_KEY } from './messages.state';

@NgModule({
  imports: [
    StoreModule.forFeature(MESSAGES_FEATURE_KEY, messagesReducer),
    EffectsModule.forFeature([MessageEffects])
  ]
})
export class MessagesStoreModule {}

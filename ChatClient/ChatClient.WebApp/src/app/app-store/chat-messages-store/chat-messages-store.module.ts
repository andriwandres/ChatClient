import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ChatMessagesEffects } from './effects';
import { chatMessageReducer } from './reducer';
import { chatMessagesFeatureKey } from './selectors';

@NgModule({
  imports: [
    StoreModule.forFeature(chatMessagesFeatureKey, chatMessageReducer),
    EffectsModule.forFeature([ChatMessagesEffects]),
  ],
  providers: [ChatMessagesEffects],
})
export class ChatMessagesStoreModule { }

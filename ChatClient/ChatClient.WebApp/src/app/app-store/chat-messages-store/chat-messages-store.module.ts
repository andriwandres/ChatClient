import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ChatMessagesEffects } from './effects';
import { chatMessageReducer } from './reducer';
import { chatMessagesFeatureKey } from './selectors';

@NgModule({
  imports: [
    StoreModule.forFeature(chatMessagesFeatureKey, chatMessageReducer),
    EffectsModule.forFeature([ChatMessagesEffects]),

    RouterModule
  ],
  providers: [ChatMessagesEffects],
})
export class ChatMessagesStoreModule { }

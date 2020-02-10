import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { LatestMessageEffects } from './effects';
import { latestMessagesReducer } from './reducer';
import { latestMessagesFeatureKey } from './selectors';



@NgModule({
  imports: [
    StoreModule.forFeature(latestMessagesFeatureKey, latestMessagesReducer),
    EffectsModule.forFeature([LatestMessageEffects]),
  ],
  providers: [LatestMessageEffects],
})
export class LatestMessagesStoreModule { }

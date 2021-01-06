import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { RecipientEffects } from './effects';
import { recipientReducer } from './reducer';
import { RECIPIENTS_FEATURE_KEY } from './state';

@NgModule({
  declarations: [],
  imports: [
    StoreModule.forFeature(RECIPIENTS_FEATURE_KEY, recipientReducer),
    EffectsModule.forFeature([RecipientEffects])
  ],
  providers: [RecipientEffects]
})
export class RecipientStoreModule { }

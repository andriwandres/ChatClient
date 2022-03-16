import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { LanguageEffects } from './language.effects';
import { languageReducer } from './language.reducer';
import { LANGUAGE_FEATURE_KEY } from './language.state';

@NgModule({
  imports: [
    StoreModule.forFeature(LANGUAGE_FEATURE_KEY, languageReducer),
    EffectsModule.forFeature([LanguageEffects])
  ]
})
export class LanguageStoreModule {}

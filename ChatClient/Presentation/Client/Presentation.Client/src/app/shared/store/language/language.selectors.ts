import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as languageState from './language.state';

// Feature selector
export const selectLanguageFeature = createFeatureSelector<languageState.State>(languageState.LANGUAGE_FEATURE_KEY);

// State selectors
export const selectError = createSelector(
  selectLanguageFeature,
  state => state.error
);

export const selectSelectedLanguageIso = createSelector(
  selectLanguageFeature,
  state => state.selectedLanguageIso
);

export const selectAvailableLanguages = createSelector(
  selectLanguageFeature,
  state => state.availableLanguages
)

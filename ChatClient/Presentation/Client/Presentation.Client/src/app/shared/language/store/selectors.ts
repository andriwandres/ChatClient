import { Language } from '@chat-client/core/models';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as languageState from './state';

// Feature selector
export const selectLanguageFeature =
  createFeatureSelector<languageState.State>(languageState.LANGUAGE_FEATURE_KEY);

// State selectors
export const selectError = createSelector(
  selectLanguageFeature,
  state => state.error
);

export const selectSelectedLanguageId = createSelector(
  selectLanguageFeature,
  state => state.selectedLanguageId
);

export const selectSelectedLanguage = createSelector(
  selectLanguageFeature,
  state => (state.selectedLanguageId && state.entities[state.selectedLanguageId]) as Language
);

export const {
  selectAll,
} = languageState.languageAdapter.getSelectors(selectLanguageFeature);

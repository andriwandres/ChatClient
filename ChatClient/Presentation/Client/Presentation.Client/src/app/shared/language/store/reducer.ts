import { createReducer, on } from '@ngrx/store';
import { initialState, languageAdapter, State } from './state';
import * as languageActions from './actions';

const reducer = createReducer(
  initialState,

  // Fetch a list of languages
  on(languageActions.getLanguagesSuccess, (state, { languages }) => languageAdapter.addMany(languages, {
    ...state,
  })),
  on(languageActions.getLanguagesFailure, (state, { error }) => ({
    ...state,
    error
  })),

  // Select preferred language
  on(languageActions.selectLanguage, (state, { languageId }) => ({
    ...state,
    selectedLanguageId: languageId
  }))
);

export function languageReducer(state: State | undefined, action: languageActions.LanguageActionUnion): State {
  return reducer(state, action);
}

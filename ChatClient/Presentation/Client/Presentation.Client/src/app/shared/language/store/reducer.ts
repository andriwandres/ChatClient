import { createReducer, on } from '@ngrx/store';
import * as languageActions from './actions';
import { initialState, State } from './state';

const reducer = createReducer(
  initialState,

  // Select preferred language
  on(languageActions.selectLanguage, (state, { languageIso }) => ({
    ...state,
    selectedLanguageIso: languageIso
  }))
);

export function languageReducer(state: State | undefined, action: languageActions.LanguageActionUnion): State {
  return reducer(state, action);
}

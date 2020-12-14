import { ApiError, Language } from '@chat-client/core/models';
import { createAction, props, union } from '@ngrx/store';

export enum LanguageActionTypes {
  GET_LANGUAGES = '[Languages] Get Languages',
  GET_LANGUAGES_SUCCESS = '[Languages] Get Languages Success',
  GET_LANGUAGES_FAILURE = '[Languages] Get Languages Failure',

  SELECT_LANGUAGE = '[Language] Select Language',
}

// Fetch list of languages
export const getLanguages = createAction(LanguageActionTypes.GET_LANGUAGES);
export const getLanguagesSuccess = createAction(LanguageActionTypes.GET_LANGUAGES_SUCCESS, props<{ languages: Language[] }>());
export const getLanguagesFailure = createAction(LanguageActionTypes.GET_LANGUAGES_FAILURE, props<{ error: ApiError | null }>());

// Select preferred language
export const selectLanguage = createAction(LanguageActionTypes.SELECT_LANGUAGE, props<{ languageId: number }>());

const allActions = union({
  getLanguages,
  getLanguagesSuccess,
  getLanguagesFailure,

  selectLanguage
});

export type LanguageActionUnion = typeof allActions;

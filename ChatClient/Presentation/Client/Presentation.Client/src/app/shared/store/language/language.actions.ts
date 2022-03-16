import { createAction, props, union } from '@ngrx/store';

export enum LanguageActionTypes {
  SELECT_LANGUAGE = '[Language] Select Language',
}

// Select preferred language
export const selectLanguage = createAction(LanguageActionTypes.SELECT_LANGUAGE, props<{ languageIso: string }>());

const allActions = union({
  selectLanguage
});

export type LanguageActionUnion = typeof allActions;

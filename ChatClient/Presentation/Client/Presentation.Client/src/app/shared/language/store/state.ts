import { ApiError } from '@chat-client/core/models';

export const LANGUAGE_FEATURE_KEY = 'languages';

export interface State {
  availableLanguages: string[];
  selectedLanguageIso: string | null;
  error: ApiError | null;
}

export interface PartialState {
  [LANGUAGE_FEATURE_KEY]: State;
}

export const initialState: State = {
  availableLanguages: ['en', 'de'],
  selectedLanguageIso: localStorage.getItem('languageIso') || 'en',
  error: null
};

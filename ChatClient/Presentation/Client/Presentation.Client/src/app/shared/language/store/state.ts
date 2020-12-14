import { ApiError, Language } from '@chat-client/core/models';
import { createEntityAdapter, EntityState } from '@ngrx/entity';

export const LANGUAGE_FEATURE_KEY = 'languages';

export interface State extends EntityState<Language> {
  selectedLanguageId: number | null;
  error: ApiError | null;
}

export interface PartialState {
  [LANGUAGE_FEATURE_KEY]: State;
}

export const languageAdapter = createEntityAdapter<Language>({
  selectId: (language) => language.languageId,
  sortComparer: (a, b) => a.languageId - b.languageId
});

export const initialState: State = languageAdapter.getInitialState({
  selectedLanguageId: +(localStorage.getItem('languageId') || 0) || null,
  error: null
});

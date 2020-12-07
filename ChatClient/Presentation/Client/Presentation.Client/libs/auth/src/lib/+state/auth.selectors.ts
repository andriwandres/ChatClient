import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthPartialState, AUTH_FEATURE_KEY, State } from './auth.state';

// Auth feature selector
export const selectAuthFeature = createFeatureSelector<AuthPartialState, State>(AUTH_FEATURE_KEY);

// State selectors
export const selectLoading = createSelector(
  selectAuthFeature,
  state => state.loading
);

export const selectError = createSelector(
  selectAuthFeature,
  state => state.error
);

export const selectUser = createSelector(
  selectAuthFeature,
  state => state.user
);

export const selectAccessToken = createSelector(
  selectAuthFeature,
  store => store.accessToken
);


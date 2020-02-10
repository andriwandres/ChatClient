import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as authState from './state';

export const authFeatureKey = 'auth';
export const selectAuthFeature = createFeatureSelector<appState.State, authState.State>(authFeatureKey);

export const selectLoading = createSelector(
  selectAuthFeature,
  (state) => state.isLoading
);

export const selectError = createSelector(
  selectAuthFeature,
  (state) => state.error,
);

export const selectUser = createSelector(
  selectAuthFeature,
  (state) => state.user,
);

export const selectToken = createSelector(
  selectAuthFeature,
  (state) => state.token,
);

export const selectEmailTaken = createSelector(
  selectAuthFeature,
  (state) => state.emailTaken,
);

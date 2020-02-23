import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as authState from './state';

// Property Name of Feature State in Root State
export const authFeatureKey = 'auth';

// Feature Selector for Auth Feature
export const selectAuthFeature = createFeatureSelector<appState.State, authState.State>(authFeatureKey);

// Selects the current Feature Loading State
export const selectLoading = createSelector(
  selectAuthFeature,
  (state) => state.isLoading
);

// Selects any errors that have occured in this Feature State
export const selectError = createSelector(
  selectAuthFeature,
  (state) => state.error,
);

// Selects the currently logged-in User
export const selectUser = createSelector(
  selectAuthFeature,
  (state) => state.user,
);

// Selects the locally stored Access Token
export const selectToken = createSelector(
  selectAuthFeature,
  (state) => state.token,
);

// Selects whether the given Email Address is taken
export const selectEmailTaken = createSelector(
  selectAuthFeature,
  (state) => state.emailTaken,
);

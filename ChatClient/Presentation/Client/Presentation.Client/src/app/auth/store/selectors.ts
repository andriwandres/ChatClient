import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as authState from './state';

// Feature selector
export const selectAuthFeature = createFeatureSelector<authState.PartialState, authState.State>(authState.AUTH_FEATURE_KEY);

export const selectLoading = createSelector(
  selectAuthFeature,
  state => state.isLoading
);

export const selectError = createSelector(
  selectAuthFeature,
  state => state.error
);

export const selectUser = createSelector(
  selectAuthFeature,
  state => state.user
);

export const selectToken = createSelector(
  selectAuthFeature,
  state => state.token
);

export const selectUserNameExists = createSelector(
  selectAuthFeature,
  state => state.userNameExists
);

export const selectEmailExists = createSelector(
  selectAuthFeature,
  state => state.emailExists
);

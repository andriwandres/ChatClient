import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as latestMessageState from './state';

export const latestMessagesFeatureKey = 'latestMessages';
export const selectLatestMessagesState = createFeatureSelector<appState.State, latestMessageState.State>(latestMessagesFeatureKey);

export const selectLoading = createSelector(
  selectLatestMessagesState,
  (state) => state.isLoading
);

export const selectError = createSelector(
  selectLatestMessagesState,
  (state) => state.error,
);

export const selectActiveMessage  = createSelector(
  selectLatestMessagesState,
  (state) => state.activeMessage
);

export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal,
} = latestMessageState.latestMessagesAdapter.getSelectors(selectLatestMessagesState);

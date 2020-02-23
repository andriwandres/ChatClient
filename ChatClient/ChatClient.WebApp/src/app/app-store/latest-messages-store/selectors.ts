import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as latestMessageState from './state';

// Property Name of Feature State in Root State
export const latestMessagesFeatureKey = 'latestMessages';

// Feature Selector for Latest Messages Feature
export const selectLatestMessagesState = createFeatureSelector<appState.State, latestMessageState.State>(latestMessagesFeatureKey);

// Selects the current Feature Loading State
export const selectLoading = createSelector(
  selectLatestMessagesState,
  (state) => state.isLoading
);

// Selects any errors that have occured in this Feature State
export const selectError = createSelector(
  selectLatestMessagesState,
  (state) => state.error,
);

// Selects the currently active message
export const selectActiveMessage  = createSelector(
  selectLatestMessagesState,
  (state) => state.activeMessage
);

// Selects specific pieces of information from the EntityStore
export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal,
} = latestMessageState.latestMessagesAdapter.getSelectors(selectLatestMessagesState);

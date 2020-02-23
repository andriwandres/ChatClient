import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as chatMessageState from './state';

// Property Name of Feature State in Root State
export const chatMessagesFeatureKey = 'chatMessages';

// Feature Selector for Chat Messages Feature
export const selectChatMessagesState = createFeatureSelector<appState.State, chatMessageState.State>(chatMessagesFeatureKey);

// Selects the current Feature Loading State
export const selectLoading = createSelector(
  selectChatMessagesState,
  (state) => state.isLoading
);

// Selects any errors that have occured in this Feature State
export const selectError = createSelector(
  selectChatMessagesState,
  (state) => state.error,
);

// Selects specific pieces of information from the EntityStore
export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal,
} = chatMessageState.chatMessagesAdapter.getSelectors(selectChatMessagesState);

import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as chatMessageState from './state';

export const chatMessagesFeatureKey = 'chatMessages';
export const selectChatMessagesState = createFeatureSelector<appState.State, chatMessageState.State>(chatMessagesFeatureKey);

export const selectLoading = createSelector(
  selectChatMessagesState,
  (state) => state.isLoading
);

export const selectError = createSelector(
  selectChatMessagesState,
  (state) => state.error,
);

export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal,
} = chatMessageState.chatMessagesAdapter.getSelectors(selectChatMessagesState);

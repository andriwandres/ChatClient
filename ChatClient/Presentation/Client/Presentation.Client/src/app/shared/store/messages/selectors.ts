import { createFeatureSelector, createSelector } from '@ngrx/store';
import { selectRecipientFeature } from 'src/app/shared/store/recipients/selectors';
import * as messagesState from './state';

export interface SelectMessagesParameter {
  recipientId: number;
}

// Feature selector
export const selectMessagesFeature = createFeatureSelector<messagesState.State>(messagesState.MESSAGES_FEATURE_KEY);

export const selectIsLoadingMessages = createSelector(
  selectMessagesFeature,
  state => state.isLoadingMessages
);

export const selectChatMessages = (recipientId: number) =>
  createSelector(
    selectMessagesFeature,
    state => state[recipientId]
  );

export const selectMessages = createSelector(
  selectMessagesFeature,
  selectRecipientFeature,
  (messageState, recipientState) => recipientState.selectedRecipientId ? messageState[recipientState.selectedRecipientId] : []
);

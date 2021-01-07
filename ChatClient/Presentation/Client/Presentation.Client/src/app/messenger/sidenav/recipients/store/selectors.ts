import { Recipient } from '@chat-client/core/models';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as recipientState from './state';

// Feature selector
export const selectRecipientFeature = createFeatureSelector<
  recipientState.PartialState,
  recipientState.State
>(recipientState.RECIPIENTS_FEATURE_KEY);

export const selectIsLoadingRecipients = createSelector(
  selectRecipientFeature,
  state => state.isLoadingRecipients
);

export const selectSelectedRecipientId = createSelector(
  selectRecipientFeature,
  state => state.selectedRecipientId
);

export const selectSelectedRecipient = createSelector(
  selectRecipientFeature,
  state => (state.selectedRecipientId && state.entities[state.selectedRecipientId]) as Recipient
);

export const {
  selectAll,
} = recipientState.recipientAdapter.getSelectors(selectRecipientFeature);

import { createReducer, on } from '@ngrx/store';
import * as recipientActions from './recipients.actions';
import { RecipientsActionUnion } from './recipients.actions';
import { initialState, recipientAdapter, State } from './recipients.state';

export function recipientReducer(state: State | undefined, action: RecipientsActionUnion): State {
  return reducer(state, action);
}

export const reducer = createReducer(
  initialState,

  // Load a list of all recipients for the current user
  on(recipientActions.loadRecipients, (state) => ({
    ...state,
    isLoadingRecipients: true,
  })),
  on(recipientActions.loadRecipientsSuccess, (state, { recipients }) =>
    recipientAdapter.addMany(recipients, {
      ...state,
      isLoadingRecipients: false,
    })
  ),
  on(recipientActions.loadRecipientsFailure, (state) => ({
    ...state,
    isLoadingRecipients: false
  })),

  // Select a recipient
  on(recipientActions.selectRecipient, (state, { recipient }) => ({
    ...state,
    selectedRecipientId: recipient.recipientId
  }))
);

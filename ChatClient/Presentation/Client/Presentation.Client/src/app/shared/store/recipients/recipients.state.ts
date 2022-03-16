import { Recipient } from '@chat-client/core/models';
import { createEntityAdapter, EntityState } from '@ngrx/entity';

export const RECIPIENTS_FEATURE_KEY = 'recipients';

export interface State extends EntityState<Recipient> {
  selectedRecipientId: number | null;
  isLoadingRecipients: boolean;
}

export interface PartialState {
  [RECIPIENTS_FEATURE_KEY]: State;
}

export const recipientAdapter = createEntityAdapter<Recipient>({
  selectId: recipient => recipient.recipientId,
});

export const initialState: State = recipientAdapter.getInitialState({
  selectedRecipientId: null,
  isLoadingRecipients: false,
});

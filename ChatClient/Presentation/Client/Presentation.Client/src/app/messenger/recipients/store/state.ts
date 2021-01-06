import { Recipient } from '@chat-client/core/models';
import { createEntityAdapter, EntityState } from '@ngrx/entity';

export const RECIPIENTS_FEATURE_KEY = 'recipients';

export interface State extends EntityState<Recipient> {
  isLoadingRecipients: boolean;
}

export interface PartialState {
  [RECIPIENTS_FEATURE_KEY]: State;
}

export const recipientAdapter = createEntityAdapter<Recipient>({
  selectId: recipient => recipient.recipientId,
  // sortComparer: (a, b) => a.latestMessage.created.getTime() - b.latestMessage.created.getTime()
});

export const initialState: State = recipientAdapter.getInitialState({
  isLoadingRecipients: false,
});

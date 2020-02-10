import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { LatestMessage } from 'src/models/messages/latest-message';

export interface State extends EntityState<LatestMessage> {
  error: any;
  isLoading: boolean;
}

export const latestMessagesAdapter = createEntityAdapter<LatestMessage>({
  selectId: m => `${m.messageId}-${m.userRecipient.userId || 0}-${m.groupRecipient.groupId || 0}`,
  sortComparer: (a, b) => a.createdAt.getTime() - b.createdAt.getTime(),
});

export const initialState: State = latestMessagesAdapter.getInitialState({
  error: null,
  isLoading: false,
});

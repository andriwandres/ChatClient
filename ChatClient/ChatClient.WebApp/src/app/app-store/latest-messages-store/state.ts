import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { LatestMessage } from 'src/models/messages/latest-message';

export interface State extends EntityState<LatestMessage> {
  error: any;
  isLoading: boolean;
  activeMessage: LatestMessage;
}

export const latestMessagesAdapter = createEntityAdapter<LatestMessage>({
  selectId: message => message.messageRecipientId,
  sortComparer: (a, b) => a.createdAt.getTime() - b.createdAt.getTime(),
});

export const initialState: State = latestMessagesAdapter.getInitialState({
  activeMessage: null,
  error: null,
  isLoading: false,
});

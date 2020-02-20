import { ChatMessage } from 'src/models/messages/chat-message';
import { EntityState, createEntityAdapter } from '@ngrx/entity';

export interface State extends EntityState<ChatMessage> {
  error: any;
  isLoading: boolean;
}

export const chatMessagesAdapter = createEntityAdapter<ChatMessage>({
  selectId: message => message.messageId,
  sortComparer: (a, b) => a.createdAt.getTime() - b.createdAt.getTime()
});

export const initialState: State = chatMessagesAdapter.getInitialState({
  error: null,
  isLoading: false,
});

import { ChatMessage } from 'src/models/messages/chat-message';
import { EntityState, createEntityAdapter } from '@ngrx/entity';

export interface State extends EntityState<ChatMessage> {
  error: any;
  isLoading: boolean;
}

// Adapter for accessing Entity-Related information
export const chatMessagesAdapter = createEntityAdapter<ChatMessage>({
  selectId: message => message.messageId,
  sortComparer: (a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
});

// Initial State upon Startup
export const initialState: State = chatMessagesAdapter.getInitialState({
  error: null,
  isLoading: false,
});

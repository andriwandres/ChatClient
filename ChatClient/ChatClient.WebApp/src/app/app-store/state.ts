import { AuthStoreState } from './auth-store';
import { ChatMessagesStoreState } from './chat-messages-store';
import { LatestMessagesStoreState } from './latest-messages-store';

export interface State {
  auth: AuthStoreState.State;
  latestMessages: LatestMessagesStoreState.State;
  chatMessages: ChatMessagesStoreState.State;
}

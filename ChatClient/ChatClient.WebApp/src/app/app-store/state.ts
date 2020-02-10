import { AuthStoreState } from './auth-store';
import { LatestMessagesStoreState } from './latest-messages-store';

export interface State {
  auth: AuthStoreState.State;
  latestMessages: LatestMessagesStoreState.State;
}

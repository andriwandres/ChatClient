import { ChatMessage } from '@chat-client/core/models';

export const MESSAGES_FEATURE_KEY = 'messages';

export interface State {
  isLoadingMessages: boolean;
  [recipientId: number]: ChatMessage[];
}

export interface PartialState {
  [MESSAGES_FEATURE_KEY]: State;
}

export const initialState: State = {
  isLoadingMessages: false
};

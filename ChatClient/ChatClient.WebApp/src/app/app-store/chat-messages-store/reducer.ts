import { createReducer, on } from '@ngrx/store';
import * as chatMessagesActions from './actions';
import { chatMessagesAdapter, initialState, State } from './state';

const reducer = createReducer(
  initialState,

  // Private Messages
  on(chatMessagesActions.loadPrivateMessages, (state, { recipientId }) => {
    return chatMessagesAdapter.removeAll({
      ...state,
      isLoading: true,
      error: null,
    });
  }),
  on(chatMessagesActions.loadPrivateMessagesSuccess, (state, { messages }) => {
    return chatMessagesAdapter.addAll(messages, {
      ...state,
      isLoading: false,
      error: null,
    });
  }),
  on(chatMessagesActions.loadPrivateMessagesFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error,
    };
  }),

  // Group Messages
  on(chatMessagesActions.loadGroupMessages, (state, { groupId }) => {
    return chatMessagesAdapter.removeAll({
      ...state,
      isLoading: true,
      error: null,
    });
  }),
  on(chatMessagesActions.loadGroupMessagesSuccess, (state, { messages }) => {
    return chatMessagesAdapter.addAll(messages, {
      ...state,
      isLoading: false,
      error: null,
    });
  }),
  on(chatMessagesActions.loadGroupMessagesFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error,
    };
  })
);

export function chatMessageReducer(state: State | undefined, action: chatMessagesActions.ChatMessagesActionUnion): State {
  return reducer(state, action);
}

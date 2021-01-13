import { createReducer, on } from '@ngrx/store';
import * as messagesActions from './actions';
import { initialState, State } from './state';

export function messagesReducer(state: State | undefined, action: messagesActions.MessagesActionUnion): State {
  return reducer(state, action);
}

export const reducer = createReducer(
  initialState,

  // Load a list messages with a recipient
  on(messagesActions.loadMessages, (state) => ({
    ...state,
    isLoadingMessages: true,
  })),

  on(
    messagesActions.loadMessagesSuccess,
    (state, { result: [recipientId, messages] }) => ({
      ...state,
      isLoadingMessages: false,
      [recipientId]: messages,
    })
  ),

  on(messagesActions.loadMessagesFailure, (state) => ({
    ...state,
    isLoadingMessages: false,
  }))
);

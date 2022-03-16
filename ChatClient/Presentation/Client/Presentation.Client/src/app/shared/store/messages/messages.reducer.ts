import { createReducer, on } from '@ngrx/store';
import * as messagesActions from './messages.actions';
import { initialState, State } from './messages.state';

export function messagesReducer(
  state: State | undefined,
  action: messagesActions.MessagesActionUnion
): State {
  return reducer(state, action);
}

export const reducer = createReducer(
  initialState,

  // Load a list messages with a recipient
  on(messagesActions.loadMessages, (state) => ({
    ...state,
    isLoadingMessages: true,
  })),

  // Store messages and replace messages completely
  on(messagesActions.loadMessagesSuccess,
    (state, { result: [recipientId, messages] }) => ({
      ...state,
      isLoadingMessages: false,
      [recipientId]: messages,
    })
  ),

  // Store messages before a certain date
  on(messagesActions.loadPreviousMessagesSuccess,
    (state, { result: [recipientId, messages] }) => ({
      ...state,
      isLoadingMessages: false,
      [recipientId]: [...messages, ...state[recipientId]],
    })
  ),

  // Add single message to the list of messages
  on(messagesActions.addMessage, (state, { recipientId, message }) => ({
    ...state,
    [recipientId]: [...state[recipientId], message],
  })),

  on(messagesActions.loadMessagesFailure, (state) => ({
    ...state,
    isLoadingMessages: false,
  }))
);

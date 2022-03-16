import { ApiError, ChatMessage, SendMessageBody } from '@chat-client/core/models';
import { createAction, props, union } from '@ngrx/store';

export enum ActionTypes {
  LOAD_MESSAGES = '[Messages] Load Messages',

  LOAD_MESSAGES_SUCCESS = '[Messages] Load Messages Success',
  LOAD_PREVIOUS_MESSAGES_SUCCESS = '[Messages] Load Previous Messages Success',

  SEND_MESSAGE = '[Messages] Send Message',

  ADD_MESSAGE = '[Messages] Add Message',

  LOAD_MESSAGES_FAILURE = '[Messages] Load Messages Failure',

  SEND_MESSAGE_FAILURE = '[Messages] Send Message Failure',
}

// Load a list of messages with a recipient
export const loadMessages = createAction(
  ActionTypes.LOAD_MESSAGES,
  props<{ recipientId: number, before?: Date }>()
);

// Load a list of messages and replace the state completely
export const loadMessagesSuccess = createAction(
  ActionTypes.LOAD_MESSAGES_SUCCESS,
  props<{ result: [recipientId: number, messages: ChatMessage[]] }>()
);

// Load a list of message with a recipient before a given date
export const loadPreviousMessagesSuccess = createAction(
  ActionTypes.LOAD_PREVIOUS_MESSAGES_SUCCESS,
  props<{ result: [recipientId: number, messages: ChatMessage[]] }>()
);

// Add a received message to the list of messages
export const addMessage = createAction(
  ActionTypes.ADD_MESSAGE,
  props<{ recipientId: number, message: ChatMessage }>()
);

// Send a new message
export const sendMessage = createAction(
  ActionTypes.SEND_MESSAGE,
  props<{ body: SendMessageBody }>()
);

export const loadMessagesFailure = createAction(
  ActionTypes.LOAD_MESSAGES_FAILURE,
  props<{ error: ApiError | null }>()
);

export const sendMessageFailure = createAction(
  ActionTypes.SEND_MESSAGE_FAILURE,
  props<{ error: ApiError | null }>()
);

const allActions = union({
  loadMessages,

  loadMessagesSuccess,
  loadPreviousMessagesSuccess,

  loadMessagesFailure,
  sendMessageFailure,

  sendMessage,
  addMessage,
});

export type MessagesActionUnion = typeof allActions;

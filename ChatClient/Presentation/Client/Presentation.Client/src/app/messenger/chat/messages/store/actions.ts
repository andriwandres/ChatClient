import { ApiError, ChatMessage } from '@chat-client/core/models';
import { createAction, props, union } from '@ngrx/store';

export enum ActionTypes {
  LOAD_MESSAGES = '[Messages] Load Messages',
  LOAD_MESSAGES_SUCCESS = '[Messages] Load Messages Success',
  LOAD_MESSAGES_FAILURE = '[Messages] Load Messages Failure',
}

// Load a list of messages with a recipient
export const loadMessages = createAction(
  ActionTypes.LOAD_MESSAGES,
  props<{ recipientId: number }>()
);
export const loadMessagesSuccess = createAction(
  ActionTypes.LOAD_MESSAGES_SUCCESS,
  props<{ messages: ChatMessage[] }>()
);
export const loadMessagesFailure = createAction(
  ActionTypes.LOAD_MESSAGES_FAILURE,
  props<{ error: ApiError | null }>()
);

const allActions = union({
  loadMessages,
  loadMessagesSuccess,
  loadMessagesFailure,
});

export type MessagesActionUnion = typeof allActions;

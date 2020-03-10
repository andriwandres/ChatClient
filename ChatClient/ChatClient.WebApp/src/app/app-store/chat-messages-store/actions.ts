import { createAction, props, union } from '@ngrx/store';
import { ChatMessage, ChatMessageDto } from 'src/models/messages/chat-message';

// Action Types for Loading/Posting Chat Messages
export enum ActionTypes {
  LOAD_PRIVATE_MESSAGES = '[Chat Messages] Load Private Messages',
  LOAD_PRIVATE_MESSAGES_SUCCESS = '[Chat Messages] Load Private Messages Success',
  LOAD_PRIVATE_MESSAGES_FAILURE = '[Chat Messages] Load Private Messages Failure',

  LOAD_GROUP_MESSAGES = '[Chat Messages] Load Group Messages',
  LOAD_GROUP_MESSAGES_SUCCESS = '[Chat Messages] Load Group Messages Success',
  LOAD_GROUP_MESSAGES_FAILURE = '[Chat Messages] Load Group Messages Failure',

  ADD_PRIVATE_MESSAGE = '[Chat Messages] Add Private Message',
  ADD_PRIVATE_MESSAGE_SUCCESS = '[Chat Messages] Add Private Message Success',
  ADD_PRIVATE_MESSAGE_FAILURE = '[Chat Messages] Add Private Message Failure',

  ADD_GROUP_MESSAGE = '[Chat Messages] Add Group Message',
  ADD_GROUP_MESSAGE_SUCCESS = '[Chat Messages] Add Group Message Success',
  ADD_GROUP_MESSAGE_FAILURE = '[Chat Messages] Add Group Message Failure',
}

// Loads Private Messages with another User
export const loadPrivateMessages = createAction(ActionTypes.LOAD_PRIVATE_MESSAGES, props<{ recipientId: number }>());
export const loadPrivateMessagesSuccess = createAction(ActionTypes.LOAD_PRIVATE_MESSAGES_SUCCESS, props<{ messages: ChatMessage[] }>());
export const loadPrivateMessagesFailure = createAction(ActionTypes.LOAD_PRIVATE_MESSAGES_FAILURE, props<{ error: any }>());

// Loads Group Messages with a Group Chat
export const loadGroupMessages = createAction(ActionTypes.LOAD_GROUP_MESSAGES, props<{ groupId: number }>());
export const loadGroupMessagesSuccess = createAction(ActionTypes.LOAD_GROUP_MESSAGES_SUCCESS, props<{ messages: ChatMessage[] }>());
export const loadGroupMessagesFailure = createAction(ActionTypes.LOAD_GROUP_MESSAGES_FAILURE, props<{ error: any }>());

// Adds new Private Message
export const addPrivateMessage = createAction(ActionTypes.ADD_PRIVATE_MESSAGE, props<{ message: ChatMessageDto }>());
export const addPrivateMessageSuccess = createAction(ActionTypes.ADD_PRIVATE_MESSAGE_SUCCESS, props<{ message: ChatMessage }>());
export const addPrivateMessageFailure = createAction(ActionTypes.ADD_PRIVATE_MESSAGE_FAILURE, props<{ error: any }>());

// Adds new Group Message
export const addGroupMessage = createAction(ActionTypes.ADD_GROUP_MESSAGE, props<{ message: ChatMessageDto }>());
export const addGroupMessageSuccess = createAction(ActionTypes.ADD_GROUP_MESSAGE_SUCCESS, props<{ message: ChatMessage }>());
export const addGroupMessageFailure = createAction(ActionTypes.ADD_GROUP_MESSAGE_FAILURE, props<{ error: any }>());

// Union Type for all Chat-Message-related Actions
const allActions = union({
  loadPrivateMessages,
  loadPrivateMessagesSuccess,
  loadPrivateMessagesFailure,
  loadGroupMessages,
  loadGroupMessagesSuccess,
  loadGroupMessagesFailure,
  addPrivateMessage,
  addPrivateMessageSuccess,
  addPrivateMessageFailure,
  addGroupMessage,
  addGroupMessageSuccess,
  addGroupMessageFailure
});

export type ChatMessagesActionUnion = typeof allActions;

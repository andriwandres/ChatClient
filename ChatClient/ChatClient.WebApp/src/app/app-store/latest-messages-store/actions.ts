import { createAction, props, union } from '@ngrx/store';
import { LatestMessage } from 'src/models/messages/latest-message';

export enum ActionTypes {
  LOAD_LATEST_MESSAGES = '[Latest Messages] Load Latest Messages',
  LOAD_LATEST_MESSAGES_SUCCESS = '[Latest Messages] Load Latest Messages Success',
  LOAD_LATEST_MESSAGES_FAILURE = '[Latest Messages] Load Latest Messages Failure',
}

export const loadLatestMessages = createAction(ActionTypes.LOAD_LATEST_MESSAGES);
export const loadLatestMessagesSuccess = createAction(ActionTypes.LOAD_LATEST_MESSAGES_SUCCESS, props<{ latestMessages: LatestMessage[] }>());
export const loadLatestMessagesFailure = createAction(ActionTypes.LOAD_LATEST_MESSAGES_FAILURE, props<{ error: any }>());

const allActions = union({
  loadLatestMessages,
  loadLatestMessagesSuccess,
  loadLatestMessagesFailure,
});

export type LatestMessagesUnion = typeof allActions;

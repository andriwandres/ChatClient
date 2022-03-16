import { ApiError, Recipient } from '@chat-client/core/models';
import { createAction, props, union } from '@ngrx/store';

// Action identifiers
export enum ActionTypes {
  LOAD_RECIPIENTS = '[Recipients] Load Recipients',
  LOAD_RECIPIENTS_SUCCESS = '[Recipients] Load Recipients Success',
  LOAD_RECIPIENTS_FAILURE = '[Recipients] Load Recipients Failure',

  SELECT_RECIPIENT = '[Recipients] Select Recipient'
}

// Load a list of all recipients
export const loadRecipients = createAction(ActionTypes.LOAD_RECIPIENTS);
export const loadRecipientsSuccess = createAction(ActionTypes.LOAD_RECIPIENTS_SUCCESS, props<{ recipients: Recipient[] }>());
export const loadRecipientsFailure = createAction(ActionTypes.LOAD_RECIPIENTS_FAILURE, props<{ error: ApiError | null }>());

// Select a recipient
export const selectRecipient = createAction(ActionTypes.SELECT_RECIPIENT, props<{ recipient: Recipient }>());

// Action Union Type
const allActions = union({
  loadRecipients,
  loadRecipientsSuccess,
  loadRecipientsFailure
});

export type RecipientsActionUnion = typeof allActions;

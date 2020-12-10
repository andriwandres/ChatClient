import { ApiError, AuthenticatedUser, CreateAccountCredentials, LoginCredentials } from '@chat-client/core/models';
import { createAction, props, union } from '@ngrx/store';

// Action identifiers
export enum ActionTypes {
  AUTHENTICATE = '[Auth] Authenticate User',
  AUTHENTICATE_SUCCESS = '[Auth] Authenticate User Success',
  AUTHENTICATE_FAILURE = '[Auth] Authenticate User Failure',

  CREATE_ACCOUNT = '[Auth] Create User Account',
  CREATE_ACCOUNT_SUCCESS = '[Auth] Create User Account Success',
  CREATE_ACCOUNT_FAILURE = '[Auth] Create User Account Failure',

  LOGIN = '[Auth] Log in User',
  LOGIN_SUCCESS = '[Auth] Log in User Success',
  LOGIN_FAILURE = '[Auth] Log in User Failure',

  LOGOUT = '[Auth] Log out',

  EMAIL_EXISTS = '[Auth] Check Email Availability',
  EMAIL_EXISTS_SUCCESS = '[Auth] Check Email Availability Success',
  EMAIL_EXISTS_FAILURE = '[Auth] Check Email Availability Failure',

  USERNAME_EXISTS = '[Auth] Check UserName Availability',
  USERNAME_EXISTS_SUCCESS = '[Auth] Check UserName Availability Success',
  USERNAME_EXISTS_FAILURE = '[Auth] Check UserName Availability Failure',

  RESET_AVAILABILITY_CHECKS = '[Auth] Reset Availability Checks'
}

// Authenticate the current user in this session
export const authenticate = createAction(ActionTypes.AUTHENTICATE);
export const authenticateSuccess = createAction(ActionTypes.AUTHENTICATE_SUCCESS, props<{ user: AuthenticatedUser }>());
export const authenticateFailure = createAction(ActionTypes.AUTHENTICATE_FAILURE, props<{ error: ApiError }>());

// Log in to a new session
export const logIn = createAction(ActionTypes.LOGIN, props<{ credentials: LoginCredentials }>());
export const logInSuccess = createAction(ActionTypes.LOGIN_SUCCESS, props<{ user: AuthenticatedUser }>());
export const logInFailure = createAction(ActionTypes.LOGIN_FAILURE, props<{ error: ApiError }>());

// Log out from the current session
export const logOut = createAction(ActionTypes.LOGOUT);

// Create a new user account
export const createAccount = createAction(ActionTypes.CREATE_ACCOUNT, props<{ credentials: CreateAccountCredentials }>());
export const createAccountSuccess = createAction(ActionTypes.CREATE_ACCOUNT_SUCCESS);
export const createAccountFailure = createAction(ActionTypes.CREATE_ACCOUNT_FAILURE, props<{ error: ApiError }>());

// Check whether a given email address already exists
export const emailExists = createAction(ActionTypes.EMAIL_EXISTS, props<{ email: string }>());
export const emailExistsSuccess = createAction(ActionTypes.EMAIL_EXISTS_SUCCESS, props<{ result: boolean }>());
export const emailExistsFailure = createAction(ActionTypes.EMAIL_EXISTS_FAILURE, props<{ error: ApiError }>());

// Check whether a given user name already exists
export const userNameExists = createAction(ActionTypes.USERNAME_EXISTS, props<{ userName: string }>());
export const userNameExistsSuccess = createAction(ActionTypes.USERNAME_EXISTS_SUCCESS, props<{ result: boolean }>());
export const userNameExistsFailure = createAction(ActionTypes.USERNAME_EXISTS_FAILURE, props<{ error: ApiError }>());

// Reset Availability Checks when navigating away from the Registration page
export const resetAvailabilityChecks = createAction(ActionTypes.RESET_AVAILABILITY_CHECKS);

// Union type for all auth actions
const allActions = union({
  authenticate,
  authenticateSuccess,
  authenticateFailure,

  logIn,
  logInSuccess,
  logInFailure,

  logOut,

  createAccount,
  createAccountSuccess,
  createAccountFailure,

  emailExists,
  emailExistsSuccess,
  emailExistsFailure,

  userNameExists,
  userNameExistsSuccess,
  userNameExistsFailure,

  resetAvailabilityChecks
});

export type AuthActionUnion = typeof allActions;

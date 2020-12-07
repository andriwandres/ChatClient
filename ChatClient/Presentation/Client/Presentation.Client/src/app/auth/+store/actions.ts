import { AuthenticatedUser, CreateAccountCredentials, LoginCredentials } from '@core/models';
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
}

// Authenticate the current user in this session
const authenticate = createAction(ActionTypes.AUTHENTICATE);
const authenticateSuccess = createAction(ActionTypes.AUTHENTICATE_SUCCESS, props<{ user: AuthenticatedUser }>());
const authenticateFailure = createAction(ActionTypes.AUTHENTICATE_FAILURE, props<{ error: string }>());

// Login to a new session
const loginAccount = createAction(ActionTypes.LOGIN, props<{ credentials: LoginCredentials }>());
const loginAccountSuccess = createAction(ActionTypes.LOGIN_SUCCESS);
const loginAccountFailure = createAction(ActionTypes.LOGIN_FAILURE, props<{ error: string }>());

// Log out from the current session
const logout = createAction(ActionTypes.LOGOUT);

// Create a new user account
const createAccount = createAction(ActionTypes.CREATE_ACCOUNT, props<{ credentials: CreateAccountCredentials }>());
const createAccountSuccess = createAction(ActionTypes.CREATE_ACCOUNT_SUCCESS);
const createAccountFailure = createAction(ActionTypes.CREATE_ACCOUNT_FAILURE, props<{ error: string }>());

// Check whether an given email address already exists
const emailExists = createAction(ActionTypes.EMAIL_EXISTS, props<{ email: string }>());
const emailExistsSuccess = createAction(ActionTypes.EMAIL_EXISTS_SUCCESS, props<{ result: boolean }>());
const emailExistsFailure = createAction(ActionTypes.EMAIL_EXISTS_FAILURE, props<{ error: string }>());

// Check whether a given user name already exists
const userNameExists = createAction(ActionTypes.USERNAME_EXISTS, props<{ userName: string }>());
const userNameExistsSuccess = createAction(ActionTypes.USERNAME_EXISTS_SUCCESS, props<{ result: boolean }>());
const userNameExistsFailure = createAction(ActionTypes.USERNAME_EXISTS_FAILURE, props<{ error: string }>());

// Union type for all auth actions
const allActions = union({
  authenticate,
  authenticateSuccess,
  authenticateFailure,

  loginAccount,
  loginAccountSuccess,
  loginAccountFailure,

  logout,

  createAccount,
  createAccountSuccess,
  createAccountFailure,

  emailExists,
  emailExistsSuccess,
  emailExistsFailure,

  userNameExists,
  userNameExistsSuccess,
  userNameExistsFailure,
});

export type AuthActionUnion = typeof allActions;

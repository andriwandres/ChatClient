import { createAction, props, union } from '@ngrx/store';
import { AuthenticatedUser, CreateAccountCredentials, LoginCredentials } from '../models/user';

// Action identifiers
export enum AT {
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
const authenticate = createAction(AT.AUTHENTICATE);
const authenticateSuccess = createAction(AT.AUTHENTICATE_SUCCESS, props<{ user: AuthenticatedUser }>());
const authenticateFailure = createAction(AT.AUTHENTICATE_FAILURE, props<{ error: any }>());

// Login to a new session
const loginAccount = createAction(AT.LOGIN, props<{ credentials: LoginCredentials }>());
const loginAccountSuccess = createAction(AT.LOGIN_SUCCESS);
const loginAccountFailure = createAction(AT.LOGIN_FAILURE, props<{ error: any }>());

// Create a new user account
const createAccount = createAction(AT.CREATE_ACCOUNT, props<{ credentials: CreateAccountCredentials }>());
const createAccountSuccess = createAction(AT.CREATE_ACCOUNT_SUCCESS);
const createAccountFailure = createAction(AT.CREATE_ACCOUNT_FAILURE, props<{ error: any }>());

// Union type for all auth actions
const allActions = union({
  authenticate,
  authenticateSuccess,
  authenticateFailure,

  loginAccount,
  loginAccountSuccess,
  loginAccountFailure,

  createAccount,
  createAccountSuccess,
  createAccountFailure,


});

export type AuthActionUnion = typeof allActions;

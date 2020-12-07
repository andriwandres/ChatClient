import { createReducer, on } from '@ngrx/store';
import * as authActions from './actions';
import { AuthActionUnion } from './actions';
import { initialState, State } from './state';

export function authReducer(state: State | undefined, action: AuthActionUnion): State {
  return reducer(state, action);
}

const reducer = createReducer(
  initialState,

  // Authenticate the current user in this session
  on(authActions.authenticate, (state) => ({
    ...state,
    isLoading: true,
    error: null,
  })),

  on(authActions.authenticateSuccess, (state, { user }) => ({
    ...state,
    isLoading: false,
    token: user.token,
    user,
  })),

  on(authActions.authenticateFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error
  })),

  // Log in to a new session
  on(authActions.login, (state) => ({
    ...state,
    isLoading: true,
    error: null
  })),

  on(authActions.loginSuccess, (state, { user }) => ({
    ...state,
    isLoading: false,
    token: user.token,
    user,
  })),

  on(authActions.loginFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error
  })),

  // Log out from the current session
  on(authActions.logout, (state) => ({
    ...state,
    token: null,
    user: null,
  })),

  // Create a new user account
  on(authActions.createAccount, (state) => ({
    ...state,
    isLoading: true,
    error: null
  })),

  on(authActions.createAccountSuccess, (state) => ({
    ...state,
    isLoading: false,
  })),

  on(authActions.createAccountFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error
  })),

  // Check whether a given email address already exists
  on(authActions.emailExists, (state) => ({
    ...state,
    isLoading: true
  })),

  on(authActions.emailExistsSuccess, (state, { result }) => ({
    ...state,
    isLoading: false,
    emailExists: result
  })),

  on(authActions.emailExistsFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error,
  })),

  // Check whether a given user name already exists
  on(authActions.userNameExists, (state) => ({
    ...state,
    isLoading: true
  })),

  on(authActions.userNameExistsSuccess, (state, { result }) => ({
    ...state,
    isLoading: false,
    userNameExists: result
  })),

  on(authActions.userNameExistsFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error,
  })),

  // Reset Availability Checks when navigating away from the Registration page
  on(authActions.resetAvailabilityChecks, (state) => ({
    ...state,
    emailExists: null,
    userNameExists: null
  }))
);

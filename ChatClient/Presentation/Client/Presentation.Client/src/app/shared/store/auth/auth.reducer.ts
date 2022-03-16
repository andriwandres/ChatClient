import { createReducer, on } from '@ngrx/store';
import * as authActions from './auth.actions';
import { AuthActionUnion } from './auth.actions';
import { initialState, State } from './auth.state';

export function authReducer(state: State | undefined, action: AuthActionUnion): State {
  return reducer(state, action);
}

const reducer = createReducer(
  initialState,

  // Authenticate the current user in this session
  on(authActions.authenticate, (state) => ({
    ...state,
    authenticationError: null
  })),

  on(authActions.authenticateSuccess, (state, { user }) => ({
      ...state,
      authenticationAttempted: true,
      token: user.token,
      user,
    })
  ),

  on(authActions.authenticateFailure, (state, { error }) => ({
    ...state,
    authenticationAttempted: true,
    authenticationError: error
  })),

  // Log in to a new session
  on(authActions.logIn, (state) => ({
    ...state,
    loginError: null
  })),

  on(authActions.logInSuccess, (state, { user }) => ({
      ...state,
      token: user.token,
      user,
    })
  ),

  on(authActions.logInFailure, (state, { error }) => ({
    ...state,
    loginError: error
  })),

  // Log out from the current session
  on(authActions.logOut, (state) => ({
    ...state,
    token: null,
    user: null,
  })),

  // Create a new user account
  on(authActions.createAccount, (state) => ({
    ...state,
    createAccountError: null
  })),

  on(authActions.createAccountSuccess, (state) => ({
    ...state,
  })),

  on(authActions.createAccountFailure, (state, { error }) => ({
    ...state,
    createAccountError: error
  })),

  // Check whether a given email address already exists
  on(authActions.emailExists, (state) => ({
    ...state,
  })),

  on(authActions.emailExistsSuccess, (state, { result }) => ({
    ...state,
    emailExists: result
  })),

  on(authActions.emailExistsFailure, (state, { error }) => ({
    ...state,
  })),

  // Check whether a given user name already exists
  on(authActions.userNameExists, (state) => ({
    ...state,
  })),

  on(authActions.userNameExistsSuccess, (state, { result }) => ({
    ...state,
    userNameExists: result
  })),

  on(authActions.userNameExistsFailure, (state, { error }) => ({
    ...state,
  })),

  // Reset Availability Checks when navigating away from the Registration page
  on(authActions.resetAvailabilityChecks, (state) => ({
    ...state,
    emailExists: null,
    userNameExists: null
  }))
);

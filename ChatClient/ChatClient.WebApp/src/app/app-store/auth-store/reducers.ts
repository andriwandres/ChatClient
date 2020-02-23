import { createReducer, on, Action } from '@ngrx/store';
import * as authActions from './actions';
import { initialState, State } from './state';

// Action reducer for auth-related actions
const reducer = createReducer(
  initialState,

  // 'Authenticate' Actions
  on(authActions.authenticate, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.authenticateSuccess, (state, { user }) => {
    return {
      ...state,
      user,
      isLoading: false,
      error: null,
    };
  }),
  on(authActions.authenticateFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error,
    };
  }),

  // 'Login' Actions
  on(authActions.login, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.loginSuccess, (state, { user }) => {
    localStorage.setItem('token', user.token);

    return {
      ...state,
      isLoading: false,
      error: null,
      user,
      token: user.token,
    };
  }),
  on(authActions.loginFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error,
    };
  }),

  // 'Register' Actions
  on(authActions.register, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.registerSuccess, (state) => {
    return {
      ...state,
      isLoading: false,
      error: null,
    };
  }),
  on(authActions.registerFailure, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),

  // 'Logout' Action
  on(authActions.logout, (state) => {
    localStorage.removeItem('token');

    return { ...state };
  }),

  // 'Check Email Availability' Actions
  on(authActions.checkEmailAvailability, (state) => {
    return {
      ...state,
      isLoading: true,
      emailTaken: null,
      error: null,
    };
  }),
  on(authActions.checkEmailAvailabilitySuccess, (state, { result }) => {
    return {
      ...state,
      isLoading: false,
      emailTaken: result,
      error: null,
    };
  }),
  on(authActions.checkEmailAvailabilityFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      emailTaken: null,
      error,
    };
  })
);

// Exported reducer function
export function authReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}

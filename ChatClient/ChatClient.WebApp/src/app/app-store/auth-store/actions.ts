import { createAction, props, union } from '@ngrx/store';
import { LoginDto } from 'src/models/auth/login';
import { RegisterDto } from 'src/models/auth/register';
import { AuthenticatedUser } from 'src/models/auth/user';

// Action Types for Authentication/Authorization
export enum ActionTypes {
  AUTHENTICATE = '[Auth] Authenticate',
  AUTHENTICATE_SUCCESS = '[Auth] Authenticate Success',
  AUTHENTICATE_FAILURE = '[Auth] Authenticate Failure',

  LOGIN = '[Auth] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Failure',

  REGISTER = '[Auth] Register',
  REGISTER_SUCCESS = '[Auth] Register Success',
  REGISTER_FAILURE = '[Auth] Register Failure',

  LOGOUT = '[Auth] Logout',

  CHECK_EMAIL_AVAILABILITY = '[Auth] Check Email Availability',
  CHECK_EMAIL_AVAILABILITY_SUCCESS = '[Auth] Check Email Availability Success',
  CHECK_EMAIL_AVAILABILITY_FAILURE = '[Auth] Check Email Availability Failure',
}

// Authenticates the user by its token
export const authenticate = createAction(ActionTypes.AUTHENTICATE);
export const authenticateSuccess = createAction(ActionTypes.AUTHENTICATE_SUCCESS, props<{ user: AuthenticatedUser }>());
export const authenticateFailure = createAction(ActionTypes.AUTHENTICATE_FAILURE, props<{ error: any }>());

// Logs the user in given their login credentials
export const login = createAction(ActionTypes.LOGIN, props<{ credentials: LoginDto }>());
export const loginSuccess = createAction(ActionTypes.LOGIN_SUCCESS, props<{ user: AuthenticatedUser }>());
export const loginFailure = createAction(ActionTypes.LOGIN_FAILURE, props<{ error: any }>());

// Registers an account given the users login credentials
export const register = createAction(ActionTypes.REGISTER, props<{ credentials: RegisterDto }>());
export const registerSuccess = createAction(ActionTypes.REGISTER_SUCCESS);
export const registerFailure = createAction(ActionTypes.REGISTER_FAILURE, props<{ error: any }>());

// Logs a user out of his current session and deletes his locally stored token
export const logout = createAction(ActionTypes.LOGOUT);

// Checks the availability of a given email address
export const checkEmailAvailability = createAction(ActionTypes.CHECK_EMAIL_AVAILABILITY, props<{ email: string }>());
export const checkEmailAvailabilitySuccess = createAction(ActionTypes.CHECK_EMAIL_AVAILABILITY_SUCCESS, props<{ result: boolean }>());
export const checkEmailAvailabilityFailure = createAction(ActionTypes.CHECK_EMAIL_AVAILABILITY_FAILURE, props<{ error: any }>());

// Union type for all auth-related actions
const allActions = union({
  authenticate,
  authenticateSuccess,
  authenticateFailure,
  login,
  loginSuccess,
  loginFailure,
  register,
  registerSuccess,
  registerFailure,
  logout,
  checkEmailAvailability,
  checkEmailAvailabilitySuccess,
  checkEmailAvailabilityFailure,
});

export type AuthActionUnion = typeof allActions;

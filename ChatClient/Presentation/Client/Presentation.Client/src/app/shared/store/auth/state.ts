import { ApiError } from '@chat-client/core/models';
import { User } from 'src/app/core/models/user';

export const AUTH_FEATURE_KEY = 'auth';

export interface State {
  token: string | null;
  user: User | null;

  emailExists: boolean | null;
  userNameExists: boolean | null;
  authenticationAttempted: boolean;

  authenticationError: ApiError | null;
  loginError: ApiError | null;
  createAccountError: ApiError | null;
}

export const initialState: State = {
  user: null,
  authenticationAttempted: false,
  emailExists: null,
  userNameExists: null,
  token: localStorage.getItem('access_token'),

  authenticationError: null,
  loginError: null,
  createAccountError: null
};

export interface PartialState {
  [AUTH_FEATURE_KEY]: State;
}

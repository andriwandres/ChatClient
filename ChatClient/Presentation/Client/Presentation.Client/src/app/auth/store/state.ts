import { ApiError } from '@chat-client/core/models';
import { User } from 'src/app/core/models/user';

export const AUTH_FEATURE_KEY = 'auth';

export interface State {
  token: string | null;
  user: User | null;
  error: ApiError | null;
  emailExists: boolean | null;
  userNameExists: boolean | null;
  authenticationAttempted: boolean;
}

export const initialState: State = {
  user: null,
  error: null,
  authenticationAttempted: false,
  emailExists: null,
  userNameExists: null,
  token: localStorage.getItem('access_token'),
};

export interface PartialState {
  [AUTH_FEATURE_KEY]: State;
}

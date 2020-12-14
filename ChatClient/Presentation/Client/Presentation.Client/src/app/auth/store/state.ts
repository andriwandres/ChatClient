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
  token: localStorage.getItem('access_token'),
  error: null,
  authenticationAttempted: false,
  emailExists: null,
  userNameExists: null
};

export interface PartialState {
  [AUTH_FEATURE_KEY]: State;
}

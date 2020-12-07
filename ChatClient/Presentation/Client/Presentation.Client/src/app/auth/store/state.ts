import { ApiError } from '@chat-client/core/models';
import { User } from 'src/app/core/models/user';

export const AUTH_FEATURE_KEY = 'auth';

export interface State {
  token: string | null;
  user: User | null;
  error: ApiError | null;
  isLoading: boolean;
  emailExists: boolean | null;
  userNameExists: boolean | null;
}

export const initialState: State = {
  user: null,
  token: null,
  error: null,
  isLoading: false,
  emailExists: null,
  userNameExists: null
};

export interface PartialState {
  [AUTH_FEATURE_KEY]: State;
}

import { User } from '@chat-client/shared/models';

export const AUTH_FEATURE_KEY = 'auth';

export interface State {
  accessToken: string;
  user: User;
  loading: boolean;
  error: string | null;
}

export const initialState: State = {
  accessToken: localStorage.getItem('access_token'),
  user: null,
  loading: false,
  error: null,
};

export interface AuthPartialState {
  readonly [AUTH_FEATURE_KEY]: State;
}

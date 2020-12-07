import { User } from 'src/app/core/models/user';

export const AUTH_FEATURE_KEY = 'auth';

export interface State {
  token: string | null;
  user: User | null;
  loading: boolean;
  error: string | null;
}

export const initialState: State = {
  user: null,
  token: null,
  loading: false,
  error: null
};

export interface PartialState {
  [AUTH_FEATURE_KEY]: State;
}

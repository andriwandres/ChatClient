import { Action, createReducer } from '@ngrx/store';
import { initialState, State } from './auth.state';

export function authReducer(state: State | undefined, action: Action) {
  return reducer(state, action);
}

const reducer = createReducer(
  initialState,

  // actions...
);

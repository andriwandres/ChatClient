import { createReducer } from '@ngrx/store';
import { AuthActionUnion } from './actions';
import { initialState, State } from './state';

export function authReducer(state: State | undefined, action: AuthActionUnion): State {
  return reducer(state, action);
}

const reducer = createReducer(
  initialState,

  // Reduce actions
);

